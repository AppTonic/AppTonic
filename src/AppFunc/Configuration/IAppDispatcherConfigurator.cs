using System;
using System.Threading.Tasks;

namespace AppFunc.Configuration
{
    public interface IAppDispatcherConfigurator
    {
        void UseDependencyResolver(IDependencyResolver dependencyResolver);

        void GlobalPipeline(Func<Action, Action> pipeline, bool supressValidation = false);


        void RegisterHandler<TRequest>(Action<TRequest> handler,
            Func<Action<TRequest>, Action<TRequest>> handlerConfig = null)
            where TRequest : IRequest;

        void RegisterHandler<TRequest, TResponse>(Func<TRequest, TResponse> handler,
            Func<Func<TRequest, TResponse>, Func<TRequest, TResponse>> handlerConfig = null)
            where TRequest : IRequest<TResponse>;

        void RegisterHandler<TRequest>(Func<TRequest, Task> handler,
            Func<Func<TRequest, Task>, Func<TRequest, Task>> handlerConfig = null)
            where TRequest : IAsyncRequest;

        void RegisterHandler<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler,
            Func<Func<TRequest, Task<TResponse>>, Func<TRequest, Task<TResponse>>> handlerConfig = null)
            where TRequest : IAsyncRequest<TResponse>;
    }
}