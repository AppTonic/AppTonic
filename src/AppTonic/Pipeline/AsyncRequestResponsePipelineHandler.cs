using System.Threading.Tasks;

namespace AppTonic.Pipeline
{
    public class AsyncRequestResponsePipelineHandler<TRequest, TResponse> : IHandleAsync<TRequest, TResponse> where TRequest : IAsyncRequest<TResponse>
    {
        private readonly IHandleAsync<TRequest, TResponse> _inner;
        private readonly IPreRequestHandler<TRequest>[] _preRequestHandlers;
        private readonly IPostRequestResponseHandler<TRequest, TResponse>[] _postRequestResponseHandlers;

        public AsyncRequestResponsePipelineHandler(
            IHandleAsync<TRequest, TResponse> inner,
            IPreRequestHandler<TRequest>[] preRequestHandlers,
            IPostRequestResponseHandler<TRequest, TResponse>[] postRequestResponseHandlers)
        {
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
            _postRequestResponseHandlers = postRequestResponseHandlers;
        }


        public async Task<TResponse> HandleAsync(TRequest request)
        {
            foreach (var preRequestHandler in _preRequestHandlers)
            {
                preRequestHandler.Handle(request);
            }
            var result = await _inner.HandleAsync(request);
            foreach (var postRequestResponseHandler in _postRequestResponseHandlers)
            {
                postRequestResponseHandler.Handle(request, result);
            }
            return result;
        }
    }
}