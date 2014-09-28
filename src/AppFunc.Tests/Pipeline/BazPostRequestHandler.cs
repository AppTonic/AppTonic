namespace AppFunc.Tests.Pipeline
{
    public class BazPostRequestHandler<TRequest> : PostRequestHandlerBase<TRequest> where TRequest : IMessage { }
}