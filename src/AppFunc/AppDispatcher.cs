using System;
using System.Threading.Tasks;
using AppFunc.Configuration;

namespace AppFunc
{
    /// <summary>
    /// A helper class to initialize a singleton instance of IAppDispatcher,
    /// as well as dispatch requests to the default singleton IAppDispatcher instance, if initialized
    /// </summary>
    public static class AppDispatcher
    {
        private static IAppDispatcher _instance;

        /// <summary>
        /// Create a static instance of IAppDispatcher (accessible from AppDispatcher.Instance). 
        /// This is the default instance that is used by the the AppDispatcher helper class.
        /// </summary>
        /// <param name="config">AppDispatcher configuration</param>
        public static void Initialize(Action<IAppDispatcherConfigurator> config = null)
        {
            _instance = AppDispatcherFactory.Create(config);
        }

        /// <summary>
        /// The default singleton instance of IAppDispatcher, if configured
        /// </summary>
        public static IAppDispatcher Instance
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