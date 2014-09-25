using System;
using System.Collections.Generic;

namespace AppFunc.Configuration
{
    public interface IDependencyResolver
    {
        object GetInstance(Type serviceType);
        object GetInstance(Type serviceType, string key);
        IEnumerable<object> GetAllInstances(Type serviceType);

        TService GetInstance<TService>() where TService : class;
        TService GetInstance<TService>(string key) where TService : class;
        IEnumerable<TService> GetAllInstances<TService>() where TService : class;

        bool TryGetInstance<TService>(out TService handlerInstance) where TService : class;
    }
}
