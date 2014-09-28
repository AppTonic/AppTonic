

namespace AppFunc.Pipeline
{
    public class RequestPipelineHandler<TRequest> : IHandle<TRequest> where TRequest : IRequest
    {
        private readonly IHandle<TRequest> _inner;
        private readonly IPreRequestHandler<TRequest>[] _preRequestHandlers;
        private readonly IPostRequestHandler<TRequest>[] _postRequestHandlers;

        public RequestPipelineHandler(IHandle<TRequest> inner, IPreRequestHandler<TRequest>[] preRequestHandlers, IPostRequestHandler<TRequest>[] postRequestHandlers)
        {
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
            _postRequestHandlers = postRequestHandlers;
        }

        public void Handle(TRequest request)
        {
            foreach (var preRequestHandler in _preRequestHandlers)
            {
                preRequestHandler.Handle(request);
            }
            _inner.Handle(request);
            foreach (var postRequestHandler in _postRequestHandlers)
            {
                postRequestHandler.Handle(request);
            }
        }
    }
}