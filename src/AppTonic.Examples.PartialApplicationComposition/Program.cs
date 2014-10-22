using System;
using AppTonic.Examples.Shared;
using AppTonic.Examples.Shared.Data;
using AppTonic.Examples.Shared.Domain;
using AppTonic.Examples.Shared.Services;

namespace AppTonic.Examples.PartialApplicationComposition
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
