using System;

namespace AppTonic.Configuration.Internal
{
    internal sealed class LambdaRequestHandler<TRequest> : IHandle<TRequest> where TRequest : IRequest
    {
        private readonly Action<TRequest> _handler;

        public LambdaRequestHandler(Action<TRequest> handler)
        {
            _handler = handler;
        }
        public void Handle(TRequest request) { _handler(request); }
    }
}