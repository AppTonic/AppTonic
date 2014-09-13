using System;
using AppFunc.CommonServiceLocator;
using AppFunc.Examples.Shared;
using AppFunc.Examples.Shared.Data;
using AppFunc.Examples.Shared.Domain;
using CommonServiceLocator.SimpleInjectorAdapter;
using SimpleInjector.Extensions;
using SimpleInjector;

namespace AppFunc.Examples.SimpleInjector
{

    class Program
    {
        static void Main()
        {
            var container = new Container();
            container.Register<IUserRepository, InMemoryUserRepository>();
            container.Register<ILogger, ConsoleLogger>();
            container.RegisterManyForOpenGeneric(typeof(IHandle<>), AppDomain.CurrentDomain.GetAssemblies());
            container.RegisterDecorator(typeof(IHandle<>), typeof(LoggingDecorator<>));

            var simpleInjectorServiceLocatorAdapter = new SimpleInjectorServiceLocatorAdapter(container);

            AppDispatcher.Initialize(app =>
            {
                app.UseCommonServiceLocator(simpleInjectorServiceLocatorAdapter);
            });

            var request = new CreateUserRequest
            {
                EmailAddress = "jane.smith@example.com",
                FullName = "Jane Smith",
                Username = "jsmith",
                WebsiteUrl = "jsmith.co"
            };

            AppDispatcher.Handle(request);

            Console.ReadLine();
        }
    }
}
