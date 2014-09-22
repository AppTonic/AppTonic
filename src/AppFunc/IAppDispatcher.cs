using System.Threading.Tasks;

namespace AppFunc
{
    public interface IAppDispatcher
    {
        /// <summary>
        /// Synchronously dispatches a request to its request handler and returns the response
        /// </summary>
        /// <typeparam name="TRequest">The type of the request, which must implement IRequest &lt;TResponse&gt;</typeparam>
        /// <typeparam name="TResponse">The type of the response, which must match the TResponse specified by TRequest</typeparam>
        /// <param name="request">The request message</param>
        /// <returns>The response from the request handler</returns>
        TResponse Handle<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;

        /// <summary>
        /// Synchronously dispatchetes a request to its request handler 
        /// </summary>
        /// <typeparam name="TRequest">The type of the request, which must implement IRequest</typeparam>
        /// <param name="request">The request message</param>
        void Handle<TRequest>(TRequest request) where TRequest : IRequest;

        /// <summary>
        /// Asynchronously dispatches a request to its request handler
        /// </summary>
        /// <typeparam name="TRequest">The type of the request, which must impelment IAsyncRequest</typeparam>
        /// <param name="request">The request message</param>
        /// <returns>The completion task</returns>
        Task HandleAsync<TRequest>(TRequest request) where TRequest : IAsyncRequest;

        /// <summary>
        /// Asynchronously dispatches a request to its request handler and returns the response
        /// </summary>
        /// <typeparam name="TRequest">The type of request, which must implement IAysncRequest&lt;TResponse&gt;</typeparam>
        /// <typeparam name="TResponse">The type of response, which must math the TResponse specified by TRequest</typeparam>
        /// <param name="request">The request message</param>
        /// <returns>The task completion result</returns>
        Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request) where TRequest : IAsyncRequest<TResponse>;
    }
}