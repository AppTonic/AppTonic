using System;
using AppFunc.Configuration;
using SimpleInjector;

namespace AppFunc.SimpleInjector
{
    public class SimpleInjectorDependencyResolver : IDependencyResolver
    {
        private readonly Container _container;

        public SimpleInjectorDependencyResolver(Container container)
        {
            _container = container;
        }

        public object GetInstance(Type type)
        {
            return _container.GetInstance(type);
        }

        public bool TryGetInstance<T>(out T instance) where T : class
        {
            instance = _container.GetInstance<T>();
            return instance != null;
        }

        public bool TryGetInstance(Type type, out object instance)
        {
            instance = GetInstance(type);
            return instance != null;
        }
    }
}
