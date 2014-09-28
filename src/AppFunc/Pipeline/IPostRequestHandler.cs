namespace AppFunc.Pipeline
{
    /// <summary>
    /// Always run after requests are processed that do not have responses
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IPostRequestHandler<in TRequest> where TRequest : IMessage
    {
        void Handle(TRequest request);
    }
}
