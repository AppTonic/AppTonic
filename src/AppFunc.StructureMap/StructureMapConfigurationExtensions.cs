using AppFunc.Configuration;
using StructureMap;

namespace AppFunc.StructureMap
{
    public static class StructureMapConfigurationExtensions
    {
        public static void UseStructureMap(this IAppFuncConfigurator configurator, IContainer container)
        {
            var dependencyResolver = new StructureMapDependencyResolver(container);
            configurator.UseDependencyResolver(dependencyResolver);
        }
    }
}