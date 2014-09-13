using System;
using AppFunc.CommonServiceLocator;
using AppFunc.Examples.Shared.Domain;
using AppFunc.Examples.Shared.Services;
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
            container.RegisterManyForOpenGeneric(typeof(IHandle<>), typeof(UserService).Assembly);
            //container.RegisterTypes(AllClasses.FromAssemblies(typeof(UserService).Assembly), WithMappings.FromAllInterfaces);

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
