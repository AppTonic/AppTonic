using System;

namespace AppFunc.Configuration
{
    public static class AppMediatorFactory
    {
        public static IAppMediator Create(Action<IAppFuncConfigurator> config = null)
        {
            var configurator = new AppFuncConfiurator();
            if (config != null)
                config(configurator);
            return configurator.BuildRequestDispatcher();
        }
    }
}