using AppFunc.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace AppFunc.CommonServiceLocator
{
    public static class CommonServiceLocatorConfigurationExtensions
    {
        public static void UseCommonServiceLocator(this IAppFuncConfigurator config, IServiceLocator serviceLocator)
        {
            config.UseDependencyResolver(new CommonServiceLocatorDependencyResolver(serviceLocator));
        }
    }
}