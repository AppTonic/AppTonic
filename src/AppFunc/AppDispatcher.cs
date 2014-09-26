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
        private static bool _initialized;

        /// <summary>
        /// Create a static instance of IAppDispatcher (accessible from AppDispatcher.Instance). 
        /// This is the default instance that is used by the the AppDispatcher helper class.
        /// </summary>
        /// <param name="config">AppDispatcher configuration</param>
        public static void Initialize(Action<IAppDispatcherConfigurator> config = null)
        {
            Instance = AppDispatcherFactory.Create(config);
            _initialized = true;
        }

        /// <summary>
        /// The default singleton instance of IAppDispatcher, if configured
        /// </summary>
        public static IAppDispatcher Instance { get; private set; }

        public static TResponse Handle<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            ThrowIfNotInitizalized();
            return Instance.Handle<TRequest, TResponse>(request);
        }

        public static void Handle<TRequest>(TRequest request) where TRequest : IRequest
        {
            ThrowIfNotInitizalized();
            Instance.Handle(request);
        }

        public static Task HandleAsync<TRequest>(TRequest request) where TRequest : IAsyncRequest
        {
            ThrowIfNotInitizalized();
            return Instance.HandleAsync(request);
        }

        public static Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IAsyncRequest<TResponse>
        {
            ThrowIfNotInitizalized();
            return Instance.HandleAsync<TRequest, TResponse>(request);
        }

        private static void ThrowIfNotInitizalized()
        {
            if (!_initialized)
                throw new InvalidOperationException("You must call AppDispatcher.Initialize before using it.");

        }
    }
}