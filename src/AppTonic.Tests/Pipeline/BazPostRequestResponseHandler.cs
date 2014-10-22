namespace AppTonic.Tests.Pipeline
{
    public class BazPostRequestResponseHandler<TRequest, TResponse> : PostRequestResponseHandlerBase<TRequest, TResponse> where TRequest : IMessage { }
}