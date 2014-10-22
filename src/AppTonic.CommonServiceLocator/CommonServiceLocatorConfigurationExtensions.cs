using AppTonic.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace AppTonic.CommonServiceLocator
{
    public static class CommonServiceLocatorConfigurationExtensions
    {
        public static void UseCommonServiceLocator(this IAppDispatcherConfigurator config, IServiceLocator serviceLocator)
        {
            config.UseDependencyResolver(new CommonServiceLocatorDependencyResolver(serviceLocator));
        }
    }
}