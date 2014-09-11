using AppFunc.Configuration;
using Autofac;

namespace AppFunc.Autofac
{
    public static class AutofacConfigurationExtensions
    {
        public static void UseAutofac(this IAppFuncConfigurator configurator, ILifetimeScope containerScope)
        {
            configurator.UseDependencyResolver(new AutofacDependencyResolver(containerScope));
        }
    }
}