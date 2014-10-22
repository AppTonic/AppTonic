using System;
using AppTonic.Pipeline;

namespace AppTonic.Tests.Pipeline
{
    public abstract class PreRequestHandlerBase<TRequest> : IPreRequestHandler<TRequest> where TRequest : IMessage
    {
        public void Handle(TRequest request)
        {
            if (request is TestRequestMessage)
            {
                (request as TestRequestMessage).PreRequestAction();
            }
            else if (request is TestRequestResponseMessage)
            {
                (request as TestRequestResponseMessage).PreRequestAction();
            }
            else if (request is TestAsyncRequestMessage)
            {
                (request as TestAsyncRequestMessage).PreRequestAction();
            }
            else if (request is TestAsyncRequestResponseMessage)
            {
                (request as TestAsyncRequestResponseMessage).PreRequestAction();
            }
            else
            {
                throw new Exception("Unexpected message.");
            }
        }
    }
}