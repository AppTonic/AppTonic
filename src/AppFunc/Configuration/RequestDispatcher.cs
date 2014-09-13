using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFunc.Configuration.Internal;

namespace AppFunc.Configuration
{
    public class RequestDispatcher : IAppDispatcher
    {
        private readonly IDependencyResolver _dependencyResolver;
        private readonly bool _hasDependencyResolver;
        private readonly Dictionary<Type, Dictionary<Type, object>> _handlers;
        private readonly Func<Action, Action> _buildPipeline;
        private readonly bool _hasPipeline;

        public RequestDispatcher(IDependencyResolver dependencyResolver, Dictionary<Type, Dictionary<Type, object>> handlers, Func<Action, Action> buildPipeline)
        {
            _dependencyResolver = dependencyResolver;
            _hasDependencyResolver = _dependencyResolver != null;
            _handlers = handlers;
            _buildPipeline = buildPipeline;
            _hasPipeline = _buildPipeline != null;
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
            TResponse response = default(TResponse);
            TryExecutePipeline(() =>
            {
                response = ResolveHandler<TRequest, TResponse, IHandle<TRequest, TResponse>>().Handle(request);
            });
            return response;
        }

        public Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request) where TRequest : IAsyncRequest<TResponse>
        {
            Task<TResponse> response = null;
            TryExecutePipeline(() =>
            {
                response = ResolveHandler<TRequest, Task<TResponse>, IHandleAsync<TRequest, TResponse>>().HandleAsync(request);
            });
            return response;
        }

        public void Handle<TRequest>(TRequest request) where TRequest : IRequest
        {
            TryExecutePipeline(() => ResolveHandler<TRequest, Unit, IHandle<TRequest>>().Handle(request));
        }

        public Task HandleAsync<TRequest>(TRequest request) where TRequest : IAsyncRequest
        {
            Task result = null;
            TryExecutePipeline(() =>
            {
                result = ResolveHandler<TRequest, Task, IHandleAsync<TRequest>>().HandleAsync(request);
            });
            return result;
        }

        private void TryExecutePipeline(Action handle)
        {
            if (_hasPipeline)
                _buildPipeline(handle)();
            else
                handle();

        }
    }
}