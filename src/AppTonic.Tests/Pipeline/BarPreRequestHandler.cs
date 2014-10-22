namespace AppTonic.Tests.Pipeline
{
    public class BarPreRequestHandler<TRequest> : PreRequestHandlerBase<TRequest> where TRequest : IMessage { }
}