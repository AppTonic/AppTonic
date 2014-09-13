using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFunc.Configuration.Internal;

namespace AppFunc.Configuration
{
    internal sealed class AppDispatcherConfigurator : IAppDispatcherConfigurator
    {
        private IDependencyResolver _dependencyResolver;

        private readonly Dictionary<Type, Dictionary<Type, object>> _handlers = new Dictionary<Type, Dictionary<Type, object>>();
        private Func<Action, Action> _pipelineConfiguration;

        public void UseDependencyResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public void DecorateHandlers(Func<Action, Action> pipeline)
        {
            if (pipeline == null)
                throw new ArgumentNullException("pipeline", "Pipeline cannot be null");
            _pipelineConfiguration = pipeline;
        }

        public void RegisterHandler<TRequest>(Action<TRequest> handler, Func<Action<TRequest>, Action<TRequest>> handlerConfig = null) where TRequest : IRequest
        {
            if (handlerConfig != null)
                handler = handlerConfig(handler);
            RegisterHandler<TRequest, Unit>(new LambdaRequestHandler<TRequest>(handler));
        }

        public void RegisterHandler<TRequest, TResponse>(Func<TRequest, TResponse> handler, Func<Func<TRequest, TResponse>, Func<TRequest, TResponse>> handlerConfig = null) where TRequest : IRequest<TResponse>
        {
            if (handlerConfig != null)
                handler = handlerConfig(handler);

            RegisterHandler<TRequest, TResponse>(new LambdaRequestResponseHandler<TRequest, TResponse>(handler));
        }

        public void RegisterHandler<TRequest>(Func<TRequest, Task> handler, Func<Func<TRequest, Task>, Func<TRequest, Task>> handlerConfig = null) where TRequest : IAsyncRequest
        {
            if (handlerConfig != null)
                handler = handlerConfig(handler);

            RegisterHandler<TRequest, Task>(new LambdaAsyncRequestHandler<TRequest>(handler));
        }

        public void RegisterHandler<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler, Func<Func<TRequest, Task<TResponse>>, Func<TRequest, Task<TResponse>>> handlerConfig = null) where TRequest : IAsyncRequest<TResponse>
        {
            if (handlerConfig != null)
                handler = handlerConfig(handler);

            RegisterHandler<TRequest, TResponse>(new LambdaAsyncRequestResponseHandler<TRequest, TResponse>(handler));
        }

        public RequestDispatcher BuildRequestDispatcher()
        {
            return new RequestDispatcher(_dependencyResolver, _handlers, _pipelineConfiguration);
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