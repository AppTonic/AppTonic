using AppFunc.Examples.Shared.Domain;

namespace AppFunc.Examples.Shared.Services
{
    public class UserService : IHandle<CreateUserRequest>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public void Handle(CreateUserRequest request)
        {
            var user = User.Create(request);
            _userRepository.Add(user);
            _userRepository.Save();
            _logger.Info("UserService: User Created");
        }
    }
}