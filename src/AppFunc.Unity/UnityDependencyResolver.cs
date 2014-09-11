using System;
using AppFunc.Configuration;
using Microsoft.Practices.Unity;

namespace AppFunc.Unity
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetInstance(Type type)
        {
            return _container.Resolve(type);
        }

        public bool TryGetInstance<T>(out T instance) where T : class
        {
            instance = _container.Resolve<T>();
            return instance != null;
        }

        public bool TryGetInstance(Type type, out object instance)
        {
            instance = GetInstance(type);
            return instance != null;
        }
    }
}