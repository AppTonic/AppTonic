using AppFunc.Examples.Shared.Domain;

namespace AppFunc.Examples.Shared.Services
{
    public class UserService : IHandle<CreateUser>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public void Handle(CreateUser request)
        {
            var user = new User { Name = request.Name };
            _userRepository.Add(user);
            _userRepository.Save();
            _logger.Info("UserService: User Created");
        }
    }
}