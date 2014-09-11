using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFunc.Configuration.Internal;
using AppFunc.DependencyResolution;

namespace AppFunc.Configuration
{
    public class RequestDispatcher : IMediator
    {
        private readonly IDependencyResolver _dependencyResolver;
        private readonly bool _hasDependencyResolver;
        private readonly Dictionary<Type, Dictionary<Type, object>> _handlers;

        public RequestDispatcher(IDependencyResolver dependencyResolver, Dictionary<Type, Dictionary<Type, object>> handlers)
        {
            _dependencyResolver = dependencyResolver;
            _hasDependencyResolver = _dependencyResolver != null;
            _handlers = handlers;
        }

        private THandler ResolveHandler<TRequest, TResponse, THandler>() where THandler : class
        {
            if (_hasDependencyResolver)
            {
                THandler handlerInstance;
                if (_dependencyResolver.TryGetInstance(out handlerInstance)) return handlerInstance;
            }

            Dictionary<Type, object> responseHandlers;
            object handler = null;
            var hasHandlerWrapper = _handlers.TryGetValue(typeof(TRequest), out responseHandlers)
                                    && responseHandlers.TryGetValue(typeof(TResponse), out handler);

            if (!hasHandlerWrapper)
                throw new InvalidOperationException(string.Format("A handler for request type '{0}' and response type '{1}' was not registered", typeof(TRequest), typeof(TResponse)));

            return handler as THandler;
        }

        public TResponse Handle<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            return ResolveHandler<TRequest, TResponse, IHandle<TRequest, TResponse>>().Handle(request);
        }

        public Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request) where TRequest : IAsyncRequest<TResponse>
        {
            return ResolveHandler<TRequest, Task<TResponse>, IHandleAsync<TRequest, TResponse>>().HandleAsync(request);
        }

        public void Handle<TRequest>(TRequest request) where TRequest : IRequest
        {
            ResolveHandler<TRequest, Unit, IHandle<TRequest>>().Handle(request);
        }

        public Task HandleAsync<TRequest>(TRequest request) where TRequest : IAsyncRequest
        {
            return ResolveHandler<TRequest, Task, IHandleAsync<TRequest>>().HandleAsync(request);
        }
    }
}