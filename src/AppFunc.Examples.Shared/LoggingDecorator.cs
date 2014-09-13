using System;

namespace AppFunc.Examples.Shared
{
    public class LoggingDecorator<TRequest> : IHandle<TRequest> where TRequest : IRequest
    {
        private readonly IHandle<TRequest> _inner;

        public LoggingDecorator(IHandle<TRequest> inner)
        {
            _inner = inner;
        }

        public void Handle(TRequest request)
        {
            Console.WriteLine("Logging Decorator: About to handle request");
            _inner.Handle(request);
            Console.WriteLine("Logging Decorator: Finished to handlin request");
        }
    }
}