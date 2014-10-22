using System;
using AppTonic.Pipeline;

namespace AppTonic.Tests.Pipeline
{
    public abstract class PostRequestHandlerBase<TRequest> : IPostRequestHandler<TRequest> where TRequest : IMessage
    {
        public void Handle(TRequest request)
        {
            if (request is TestRequestMessage)
            {
                (request as TestRequestMessage).PostRequestAction();
            }
            else if (request is TestRequestResponseMessage)
            {
                (request as TestRequestResponseMessage).PostRequestAction();
            }
            else if (request is TestAsyncRequestMessage)
            {
                (request as TestAsyncRequestMessage).PostRequestAction();
            }
            else if (request is TestAsyncRequestResponseMessage)
            {
                (request as TestAsyncRequestResponseMessage).PostRequestAction();
            }
            else
            {
                throw new Exception("Unexpected message.");
            }
        }
    }


    public abstract class PostRequestResponseHandlerBase<TRequest, TResponse> : IPostRequestResponseHandler<TRequest, TResponse> where TRequest : IMessage
    {
        public void Handle(TRequest request, TResponse response)
        {
            if (request is TestRequestMessage)
            {
                (request as TestRequestMessage).PostRequestAction();
            }
            else if (request is TestRequestResponseMessage)
            {
                (request as TestRequestResponseMessage).PostRequestAction();
            }
            else if (request is TestAsyncRequestMessage)
            {
                (request as TestAsyncRequestMessage).PostRequestAction();
            }
            else if (request is TestAsyncRequestResponseMessage)
            {
                (request as TestAsyncRequestResponseMessage).PostRequestAction();
            }
            else
            {
                throw new Exception("Unexpected message.");
            }
        }
    }
}