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

        public bool TryGetInstance<TService>(out TService handlerInstance) where TService : class
        {
            try
            {
                handlerInstance = _serviceLocator.GetInstance<TService>();
                return handlerInstance != null;
            }
            catch (ActivationException)
            {
                handlerInstance = null;
                return false;
            }
        }
    }
}
