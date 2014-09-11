using System.Threading.Tasks;

namespace AppFunc
{
    // Request marker interfaces
    public interface IRequest { }
    public interface IRequest<TResponse> { }

    // Async request marker interfaces
    public interface IAsyncRequest : IRequest<Task> { }
    public interface IAsyncRequest<TResponse> : IRequest<Task<TResponse>> { }

    // Command marker interfaces 
    public interface ICommand : IRequest { }
    public interface ICommand<TCommandResponse> : IRequest<TCommandResponse> { }
    public interface IAsyncCommand : IAsyncRequest { }
    public interface IAsyncCommand<TCommandResponse> : IAsyncRequest<TCommandResponse> { }

    // Query marker interfaces
    public interface IQuery<TQueryResult> : IRequest<TQueryResult> { }
    public interface IAsyncQuery<TQueryResult> : IAsyncRequest<TQueryResult> { }
}