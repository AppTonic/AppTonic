using System;
using System.Threading.Tasks;

namespace AppFunc.Configuration.Internal
{
    internal sealed class LambdaAsyncRequestHandler<TRequest> : IHandleAsync<TRequest> where TRequest : IAsyncRequest
    {
        private readonly Func<TRequest, Task> _handler;

        public LambdaAsyncRequestHandler(Func<TRequest, Task> handler)
        {
            _handler = handler;
        }
        public Task HandleAsync(TRequest request) { return _handler(request); }
    }
}