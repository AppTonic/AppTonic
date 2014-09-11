using AppFunc.Configuration;
using Microsoft.Practices.Unity;

namespace AppFunc.Unity
{
    public static class UnityConfiugationExtensions
    {
        public static void UseUnity(this IAppFuncConfigurator configurator, IUnityContainer container)
        {
            configurator.UseDependencyResolver(new UnityDependencyResolver(container));
        }
    }
}
