using AppTonic.CommonServiceLocator;
using AppTonic.Examples.Shared;
using AppTonic.Examples.Shared.Data;
using AppTonic.Examples.Shared.Domain;
using AppTonic.Examples.Shared.Services;
using Microsoft.Practices.Unity;

namespace AppTonic.Examples.Unity
{
    class Program
    {
        static void Main()
        {
            // Configure your DI container
            var container = new UnityContainer();
            container.RegisterType<IUserRepository, InMemoryUserRepository>();
            container.RegisterType<ILogger, ConsoleLogger>();
            container.RegisterType(typeof(IHandle<CreateUser>), typeof(UserService));
            // Create common service locator adapter
            var unityServiceLocatorAdapter = new UnityServiceLocator(container);

            // Initilaize the AppDispatcher
            AppDispatcher.Initialize(app =>
            {
                app.UseCommonServiceLocator(unityServiceLocatorAdapter);
            });

            // Create a request
            var request = new CreateUser { Name = "Jane Smith" };

            // Handle it using the dispatcher, if DI is configured correctly
            // the message handler in your application service will handle it.
            AppDispatcher.Handle(request);
        }
    }
}