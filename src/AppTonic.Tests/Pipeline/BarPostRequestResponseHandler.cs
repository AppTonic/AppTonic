namespace AppTonic.Tests.Pipeline
{
    public class BarPostRequestResponseHandler<TRequest, TResponse> : PostRequestResponseHandlerBase<TRequest, TResponse> where TRequest : IMessage { }
}