

namespace AppFunc.Pipeline
{
    public class RequestResponsePipelineHandler<TRequest, TResponse> : IHandle<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IHandle<TRequest, TResponse> _inner;
        private readonly IPreRequestHandler<TRequest>[] _preRequestHandlers;
        private readonly IPostRequestResponseHandler<TRequest, TResponse>[] _postRequestResponseHandlers;

        public RequestResponsePipelineHandler(IHandle<TRequest, TResponse> inner, IPreRequestHandler<TRequest>[] preRequestHandlers, IPostRequestResponseHandler<TRequest, TResponse>[] postRequestResponseHandlers)
        {
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
            _postRequestResponseHandlers = postRequestResponseHandlers;
        }

        public TResponse Handle(TRequest request)
        {
            foreach (var preRequestHandler in _preRequestHandlers)
            {
                preRequestHandler.Handle(request);
            }
            var result = _inner.Handle(request);
            foreach (var postRequestResponseHandler in _postRequestResponseHandlers)
            {
                postRequestResponseHandler.Handle(request, result);
            }
            return result;
        }
    }
}