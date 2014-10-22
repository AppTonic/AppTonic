using System;
using System.Threading.Tasks;

namespace AppTonic.Configuration
{
    public interface IAppDispatcherConfigurator
    {
        /// <summary>
        /// Use the specified dependecy resolver. Install NuGet package AppTonic.CommonServiceLocator 
        /// to use the CommonServiceLocatorDependencyResolver, or implement your own.
        /// </summary>
        /// <param name="dependencyResolver"></param>
        void UseDependencyResolver(IDependencyResolver dependencyResolver);

        /// <summary>
        /// Configure the global pipeline. Ensure the inner action is invoked all the way down, otherwise
        /// the request dispatcher will fail.
        /// </summary>
        /// <param name="pipeline">The pipeline configuration function, the Action paremeter represents the next pipeline action to be invoked.</param>
        /// <param name="supressValidation">Suppresses an initial firing of the pipeline that validates it is configured correctly.</param>
        void GlobalPipeline(Func<Action, Action> pipeline, bool supressValidation = false);


        /// <summary>
        /// Registers a void request handler
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="handler"></param>
        /// <param name="handlerConfig"></param>
        void RegisterHandler<TRequest>(Action<TRequest> handler,
            Func<Action<TRequest>, Action<TRequest>> handlerConfig = null)
            where TRequest : IRequest;

        /// <summary>
        /// Registers a request/response handler
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="handler"></param>
        /// <param name="handlerConfig"></param>
        void RegisterHandler<TRequest, TResponse>(Func<TRequest, TResponse> handler,
            Func<Func<TRequest, TResponse>, Func<TRequest, TResponse>> handlerConfig = null)
            where TRequest : IRequest<TResponse>;

        /// <summary>
        /// Registers an async request handler
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="handler"></param>
        /// <param name="handlerConfig"></param>
        void RegisterHandler<TRequest>(Func<TRequest, Task> handler,
            Func<Func<TRequest, Task>, Func<TRequest, Task>> handlerConfig = null)
            where TRequest : IAsyncRequest;

        /// <summary>
        /// Registers an async request/response handler
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="handler"></param>
        /// <param name="handlerConfig"></param>
        void RegisterHandler<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler,
            Func<Func<TRequest, Task<TResponse>>, Func<TRequest, Task<TResponse>>> handlerConfig = null)
            where TRequest : IAsyncRequest<TResponse>;
    }
}