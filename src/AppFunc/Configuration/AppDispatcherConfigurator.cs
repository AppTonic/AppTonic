using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFunc.Configuration.Internal;

namespace AppFunc.Configuration
{
    internal sealed class AppDispatcherConfigurator : IAppDispatcherConfigurator
    {
        private IDependencyResolver _dependencyResolver;
        private bool _pipelineConfigured = false;
        private bool _surpressPipelineValidation;

        private readonly Dictionary<Type, Dictionary<Type, object>> _handlers = new Dictionary<Type, Dictionary<Type, object>>();
        private Func<Action, Action> _pipeline;

        public void UseDependencyResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }
        public void GlobalPipeline(Func<Action, Action> pipeline, bool supressValidation = false)
        {
            _pipeline = pipeline;
            _pipelineConfigured = true;
            _surpressPipelineValidation = supressValidation;
        }

        public void RegisterHandler<TRequest>(Action<TRequest> handler, Func<Action<TRequest>, Action<TRequest>> handlerConfig = null) where TRequest : IRequest
        {
            if (handlerConfig != null)
                handler = handlerConfig(handler);
            RegisterHandler<TRequest, Unit>(handler != null ? new LambdaRequestHandler<TRequest>(handler) : null);
        }

        public void RegisterHandler<TRequest, TResponse>(Func<TRequest, TResponse> handler, Func<Func<TRequest, TResponse>, Func<TRequest, TResponse>> handlerConfig = null) where TRequest : IRequest<TResponse>
        {
            if (handlerConfig != null)
                handler = handlerConfig(handler);

            RegisterHandler<TRequest, TResponse>(handler != null ? new LambdaRequestResponseHandler<TRequest, TResponse>(handler) : null);
        }

        public void RegisterHandler<TRequest>(Func<TRequest, Task> handler, Func<Func<TRequest, Task>, Func<TRequest, Task>> handlerConfig = null) where TRequest : IAsyncRequest
        {
            if (handlerConfig != null)
                handler = handlerConfig(handler);

            RegisterHandler<TRequest, Task>(handler != null ? new LambdaAsyncRequestHandler<TRequest>(handler) : null);
        }

        public void RegisterHandler<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler, Func<Func<TRequest, Task<TResponse>>, Func<TRequest, Task<TResponse>>> handlerConfig = null) where TRequest : IAsyncRequest<TResponse>
        {
            if (handlerConfig != null)
                handler = handlerConfig(handler);

            RegisterHandler<TRequest, Task<TResponse>>(handler != null ? new LambdaAsyncRequestResponseHandler<TRequest, TResponse>(handler) : null);
        }

        public RequestDispatcher BuildRequestDispatcher()
        {
            Validate();
            return new RequestDispatcher(_dependencyResolver, _handlers, _pipeline);
        }

        private void Validate()
        {
            Type requestType;
            Type responseType;
            foreach (var requestHandlers in _handlers)
            {
                requestType = requestHandlers.Key;
                foreach (var handler in requestHandlers.Value)
                {
                    responseType = handler.Key;
                    if (handler.Value == null)
                        throw new Exception(string.Format("The handler for {0} ({1}) cannot be null.", requestType, responseType));
                }
            }


            // Validate pipeline
            if (_pipelineConfigured && !_surpressPipelineValidation)
            {
                if (_pipeline == null)
                    throw new Exception("The global pipeline was configured but it was set to null");

                var passesThrough = false;
                _pipeline(() =>
                {
                    Trace.TraceInformation("Validating dispatcher pipeline.");
                    passesThrough = true;
                })();

                if (!passesThrough)
                    throw new Exception(
                        "The pipleine is not configured correctly. Ensure you invoking the inner handler in each pipeline extension.");
            }
        }

        private void RegisterHandler<TRequest, TResponse>(object handler)
        {
            var requestType = typeof(TRequest);
            var responseType = typeof(TResponse);
            if (!_handlers.ContainsKey(requestType))
                _handlers[requestType] = new Dictionary<Type, object>();

            if (_handlers[requestType].ContainsKey(responseType))
                throw new Exception("That type of handler has already been registered");

            _handlers[requestType][responseType] = handler;
        }
    }
}