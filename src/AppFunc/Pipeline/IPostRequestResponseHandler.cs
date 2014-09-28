namespace AppFunc.Pipeline
{
    /// <summary>
    /// Always run after requests are processed that have responses
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IPostRequestResponseHandler<in TRequest, in TResponse> where TRequest : IMessage
    {
        void Handle(TRequest request, TResponse response);
    }
}