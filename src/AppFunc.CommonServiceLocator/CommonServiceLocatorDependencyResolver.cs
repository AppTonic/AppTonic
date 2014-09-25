using System;
using System.Collections.Generic;
using AppFunc.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace AppFunc.CommonServiceLocator
{
    public class CommonServiceLocatorDependencyResolver : IDependencyResolver
    {
        private readonly IServiceLocator _serviceLocator;

        public CommonServiceLocatorDependencyResolver(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public object GetInstance(Type serviceType)
        {
            return _serviceLocator.GetInstance(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _serviceLocator.GetInstance(serviceType, key);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _serviceLocator.GetAllInstances(serviceType);
        }

        public TService GetInstance<TService>() where TService : class
        {
            return _serviceLocator.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key) where TService : class
        {
            return _serviceLocator.GetInstance<TService>(key);
        }

        public IEnumerable<TService> GetAllInstances<TService>() where TService : class
        {
            return _serviceLocator.GetAllInstances<TService>();
        }

        public bool TryGetInstance<TService>(out TService handlerInstance) where TService : class
        {
            try
            {
                handlerInstance = GetInstance<TService>();
                return handlerInstance != null;
            }
            catch (ActivationException)
            {
                handlerInstance = default(TService);
                return false;
            }
        }
    }
}
