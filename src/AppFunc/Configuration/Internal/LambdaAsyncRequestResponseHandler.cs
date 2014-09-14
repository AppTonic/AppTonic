using System;
using System.Threading.Tasks;

namespace AppFunc.Configuration.Internal
{
    internal sealed class LambdaAsyncRequestResponseHandler<TRequest, TResponse> : IHandleAsync<TRequest, TResponse> where TRequest : IAsyncRequest<TResponse>
    {
        private readonly Func<TRequest, Task<TResponse>> _handler;

        public LambdaAsyncRequestResponseHandler(Func<TRequest, Task<TResponse>> handler)
        {
            _handler = handler;
        }
        public Task<TResponse> HandleAsync(TRequest request) { return _handler(request); }
    }
}