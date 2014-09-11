using System.Threading.Tasks;

namespace AppFunc
{
    public interface IHandle<in TRequest> where TRequest : IRequest
    {
        void Handle(TRequest request);
    }

    public interface IHandle<in TRequest, out TResponse> where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest request);
    }

    public interface IHandleAsync<in TRequest> where TRequest : IAsyncRequest
    {
        Task HandleAsync(TRequest request);
    }

    public interface IHandleAsync<in TRequest, TResponse> where TRequest : IAsyncRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}