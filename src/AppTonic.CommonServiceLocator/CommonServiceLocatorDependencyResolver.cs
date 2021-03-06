﻿using AppTonic.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace AppTonic.CommonServiceLocator
{
    public class CommonServiceLocatorDependencyResolver : IDependencyResolver
    {
        private readonly IServiceLocator _serviceLocator;
        private readonly bool _throwErrors;

        public CommonServiceLocatorDependencyResolver(IServiceLocator serviceLocator, bool throwErrors = false)
        {
            _serviceLocator = serviceLocator;
            _throwErrors = throwErrors;
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
