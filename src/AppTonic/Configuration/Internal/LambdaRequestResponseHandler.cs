using System;

namespace AppTonic.Configuration.Internal
{
    internal sealed class LambdaRequestResponseHandler<TRequest, TResponse> : IHandle<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Func<TRequest, TResponse> _handler;

        public LambdaRequestResponseHandler(Func<TRequest, TResponse> handler)
        {
            _handler = handler;
        }
        public TResponse Handle(TRequest request) { return _handler(request); }
    }
}