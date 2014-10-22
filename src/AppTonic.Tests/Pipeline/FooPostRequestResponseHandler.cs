namespace AppTonic.Tests.Pipeline
{
    public class FooPostRequestResponseHandler<TRequest, TResponse> : PostRequestResponseHandlerBase<TRequest, TResponse> where TRequest : IMessage { }
}