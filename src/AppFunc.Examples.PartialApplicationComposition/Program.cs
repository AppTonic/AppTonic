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
            // Look ma, no DI container! 
            var logger = new ConsoleLogger();
            Func<IUserRepository> userRepositoryFactory =
                () => new InMemoryUserRepository();

            AppDispatcher.Initialize(app =>
            {
                app.RegisterHandler<CreateUser>(createUserRequest =>
                {
                    UserServiceModule.CreateUser(createUserRequest,
                        userRepositoryFactory, logger);
                });
            });

            var request = new CreateUser { Name = "Jane Smith" };

            AppDispatcher.Handle(request);
        }
    }
}
