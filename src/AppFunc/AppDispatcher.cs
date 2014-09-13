using System;
using System.Threading.Tasks;
using AppFunc.Configuration;

namespace AppFunc
{
    public static class AppDispatcher
    {
        private static IAppDispatcher _instance;

        public static void Initialize(Action<IAppDispatcherConfigurator> config = null)
        {
            _instance = AppMediatorFactory.Create(config);
        }

        private static IAppDispatcher Instance
        {
            get
            {
                if (_instance == null)
                    throw new InvalidOperationException("You must call AppDispatcher.Initialize before using it.");

                return _instance;
            }
        }

        public static TResponse Handle<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            return Instance.Handle<TRequest, TResponse>(request);
        }

        public static void Handle<TRequest>(TRequest request) where TRequest : IRequest
        {
            Instance.Handle(request);
        }

        public static Task HandleAsync<TRequest>(TRequest request) where TRequest : IAsyncRequest
        {
            return Instance.HandleAsync(request);
        }

        public static Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IAsyncRequest<TResponse>
        {
            return Instance.HandleAsync<TRequest, TResponse>(request);
        }
    }
}