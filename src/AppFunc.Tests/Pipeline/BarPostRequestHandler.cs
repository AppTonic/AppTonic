namespace AppFunc.Tests.Pipeline
{
    public class BarPostRequestHandler<TRequest> : PostRequestHandlerBase<TRequest> where TRequest : IMessage { }
}