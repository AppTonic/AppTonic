using System.Threading.Tasks;

namespace AppFunc.Pipeline
{
    public class AsyncRequestPipelineHandler<TRequest, TResponse> : IHandleAsync<TRequest> where TRequest : IAsyncRequest
    {
        private readonly IHandleAsync<TRequest> _inner;
        private readonly IPreRequestHandler<TRequest>[] _preRequestHandlers;
        private readonly IPostRequestHandler<TRequest>[] _postRequestHandlers;

        public AsyncRequestPipelineHandler(
            IHandleAsync<TRequest> inner,
            IPreRequestHandler<TRequest>[] preRequestHandlers,
            IPostRequestHandler<TRequest>[] postRequestHandlers)
        {
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
            _postRequestHandlers = postRequestHandlers;
        }


        public async Task HandleAsync(TRequest request)
        {
            foreach (var preRequestHandler in _preRequestHandlers)
            {
                preRequestHandler.Handle(request);
            }
            await _inner.HandleAsync(request);
            foreach (var postRequestHandler in _postRequestHandlers)
            {
                postRequestHandler.Handle(request);
            }
        }
    }
}