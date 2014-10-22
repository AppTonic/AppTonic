namespace AppTonic.Pipeline
{

    /// <summary>
    /// Run before ALL requests are handled
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IPreRequestHandler<in TRequest> where TRequest : IMessage
    {
        void Handle(TRequest request);
    }
}