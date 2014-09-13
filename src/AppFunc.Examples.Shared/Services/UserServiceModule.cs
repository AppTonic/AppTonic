using System;
using AppFunc.Examples.Shared.Domain;

namespace AppFunc.Examples.Shared.Services
{
    public static class UserServiceModule
    {
        public static void CreateUser(CreateUserRequest request, Func<IUserRepository> userRepositoryFactory, ILogger logger)
        {
            var userRepository = userRepositoryFactory();
            var user = User.Create(request);
            userRepository.Add(user);
            userRepository.Save();
            logger.Info("UserServiceModule: User Created");
        }
    }
}