using System;
using AppFunc.Configuration;
using Autofac;

namespace AppFunc.Autofac
{
    public class AutofacDependencyResolver : IDependencyResolver
    {
        private readonly ILifetimeScope _containerScope;

        public AutofacDependencyResolver(ILifetimeScope containerScope)
        {
            _containerScope = containerScope;
        }

        public object GetInstance(Type type)
        {
            return _containerScope.Resolve(type);
        }

        public bool TryGetInstance<T>(out T instance) where T : class
        {
            instance = _containerScope.Resolve<T>();
            return instance != null;
        }

        public bool TryGetInstance(Type type, out object instance)
        {
            instance = GetInstance(type);
            return instance != null;
        }
    }
}
