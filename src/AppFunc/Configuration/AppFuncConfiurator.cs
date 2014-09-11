using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFunc.Configuration.Internal;

namespace AppFunc.Configuration
{
    internal sealed class AppFuncConfiurator : IAppFuncConfigurator
    {
        private IDependencyResolver _dependencyResolver;
        private readonly Dictionary<Type, Dictionary<Type, object>> _handlers = new Dictionary<Type, Dictionary<Type, object>>();

        public void UseDependencyResolver(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public void Register<TRequest>(Action<TRequest> handler) where TRequest : IRequest
        {
            RegisterHandler<TRequest, Unit>(new LambdaRequestHandler<TRequest>(handler));
        }

        public void Register<TRequest, TResponse>(Func<TRequest, TResponse> handler) where TRequest : IRequest<TResponse>
        {
            RegisterHandler<TRequest, TResponse>(new LambdaRequestResponseHandler<TRequest, TResponse>(handler));
        }

        public void Register<TRequest>(Func<TRequest, Task> handler) where TRequest : IAsyncRequest
        {
            RegisterHandler<TRequest, Task>(new LambdaAsyncRequestHandler<TRequest>(handler));
        }

        public void Register<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler) where TRequest : IAsyncRequest<TResponse>
        {
            RegisterHandler<TRequest, TResponse>(new LambdaAsyncRequestResponseHandler<TRequest, TResponse>(handler));
        }

        public RequestDispatcher BuildRequestDispatcher()
        {
            return new RequestDispatcher(_dependencyResolver, _handlers);
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