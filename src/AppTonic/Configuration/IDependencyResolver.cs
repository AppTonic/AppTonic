namespace AppTonic.Configuration
{
    public interface IDependencyResolver
    {
        bool TryGetInstance<TService>(out TService handlerInstance) where TService : class;
    }
}
