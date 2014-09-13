using AppFunc.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace AppFunc.CommonServiceLocator
{
    public static class CommonServiceLocatorConfigurationExtensions
    {
        public static void UseCommonServiceLocator(this IAppDispatcherConfigurator config, IServiceLocator serviceLocator)
        {
            config.UseDependencyResolver(new CommonServiceLocatorDependencyResolver(serviceLocator));
        }
    }
}