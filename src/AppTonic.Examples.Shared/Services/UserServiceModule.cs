using System;
using AppTonic.Examples.Shared.Domain;

namespace AppTonic.Examples.Shared.Services
{
    public static class UserServiceModule
    {
        public static void CreateUser(CreateUser request, Func<IUserRepository> userRepositoryFactory, ILogger logger)
        {
            using (var userRepository = userRepositoryFactory())
            {
                var user = new User { Name = request.Name };
                userRepository.Add(user);
                userRepository.Save();
                logger.Info("UserServiceModule: User Created");
            }
        }
    }
}