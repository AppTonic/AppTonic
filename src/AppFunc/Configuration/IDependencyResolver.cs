using System;
using System.Collections.Generic;

namespace AppFunc.Configuration
{
    public interface IDependencyResolver
    {
        object GetInstance(Type serviceType);
        object GetInstance(Type serviceType, string key);
        IEnumerable<object> GetAllInstances(Type serviceType);

        TService GetInstance<TService>();
        TService GetInstance<TService>(string key);
        IEnumerable<TService> GetAllInstances<TService>();

        bool TryGetInstance<TService>(out TService handlerInstance);
    }
}
