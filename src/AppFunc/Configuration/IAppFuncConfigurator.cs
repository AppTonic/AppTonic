using System;
using System.Threading.Tasks;
using AppFunc.DependencyResolution;

namespace AppFunc.Configuration
{
    public interface IAppFuncConfigurator
    {
        void UseDependencyResolver(IDependencyResolver dependencyResolver);
        void Register<TRequest>(Action<TRequest> handler) where TRequest : IRequest;
        void Register<TRequest, TResponse>(Func<TRequest, TResponse> handler) where TRequest : IRequest<TResponse>;
        void Register<TRequest>(Func<TRequest, Task> handler) where TRequest : IAsyncRequest;
        void Register<TRequest, TResponse>(Func<TRequest, Task<TResponse>> handler) where TRequest : IAsyncRequest<TResponse>;
    }
}