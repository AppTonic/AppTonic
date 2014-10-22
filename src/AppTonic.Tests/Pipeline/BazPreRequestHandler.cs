namespace AppTonic.Tests.Pipeline
{
    public class BazPreRequestHandler<TRequest> : PreRequestHandlerBase<TRequest> where TRequest : IMessage { }
}