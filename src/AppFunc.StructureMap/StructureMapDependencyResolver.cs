using System;
using AppFunc.Configuration;
using StructureMap;

namespace AppFunc.StructureMap
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetInstance(Type type)
        {
            return _container.GetInstance(type);
        }

        public bool TryGetInstance<T>(out T instance) where T : class
        {
            instance = _container.TryGetInstance<T>();
            return instance != null;
        }

        public bool TryGetInstance(Type type, out object instance)
        {
            instance = _container.TryGetInstance(type);
            return instance != null;
        }
    }
}