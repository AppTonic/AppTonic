using System.Threading.Tasks;

namespace AppTonic
{
    public interface IMessage { }

    // Request marker interfaces
    public interface IRequest : IMessage { }
    public interface IRequest<TResponse> : IMessage { }

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