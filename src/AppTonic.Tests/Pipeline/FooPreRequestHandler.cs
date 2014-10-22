namespace AppTonic.Tests.Pipeline
{
    public class FooPreRequestHandler<TRequest> : PreRequestHandlerBase<TRequest> where TRequest : IMessage { }
}