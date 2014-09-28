namespace AppFunc.Tests.Pipeline
{
    public class FooPreRequestHandler<TRequest> : PreRequestHandlerBase<TRequest> where TRequest : IMessage { }
}