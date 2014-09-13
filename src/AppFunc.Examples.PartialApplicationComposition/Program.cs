using System;
using AppFunc.Examples.Shared;
using AppFunc.Examples.Shared.Data;
using AppFunc.Examples.Shared.Domain;
using AppFunc.Examples.Shared.Services;

namespace AppFunc.Examples.PartialApplicationComposition
{
    class Program
    {
        static void Main()
        {
            var logger = new ConsoleLogger();

            AppMediator.Initialize(app =>
            {
                app.Register<CreateUserRequest>(createUser =>
                {
                    UserServiceModule.CreateUser(createUser, () => new InMemoryUserRepository(), logger);
                });
            });

            var request = new CreateUserRequest
            {
                EmailAddress = "jane.smith@example.com",
                FullName = "Jane Smith",
                Username = "jsmith",
                WebsiteUrl = "jsmith.co"
            };

            AppMediator.Handle(request);

            Console.ReadLine();
        }
    }
}
