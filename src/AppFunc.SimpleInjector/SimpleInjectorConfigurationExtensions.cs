using AppFunc.Configuration;
using SimpleInjector;

namespace AppFunc.SimpleInjector
{
    public static class SimpleInjectorConfigurationExtensions
    {
        public static void UseSimpleInjector(this IAppFuncConfigurator configurator, Container container)
        {
            configurator.UseDependencyResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}