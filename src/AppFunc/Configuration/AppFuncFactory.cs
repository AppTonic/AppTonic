using System;

namespace AppFunc.Configuration
{
    public static class AppFuncFactory
    {
        public static IMediator Create(Action<IAppFuncConfigurator> config = null)
        {
            var configurator = new AppFuncConfiurator();
            if (config != null)
                config(configurator);
            return configurator.BuildRequestDispatcher();
        }
    }
}