using System;

namespace AppFunc.Configuration
{
    public interface IDependencyResolver
    {
        object GetInstance(Type type);
        bool TryGetInstance<T>(out T instance) where T : class;
        bool TryGetInstance(Type type, out object instance);
    }
}
