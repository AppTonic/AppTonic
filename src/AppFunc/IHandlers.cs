using System;
using System.Threading.Tasks;
using AppFunc;

namespace AppFunc
{
    public interface IHandle<in TRequest> where TRequest : IRequest
    {
        void Handle(TRequest request);
    }

    public interface IHandle<in TRequest, out TResponse> where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest request);
    }

    public interface IHandleAsync<in TRequest> where TRequest : IAsyncRequest
    {
        Task HandleAsync(TRequest request);
    }

    public interface IHandleAsync<in TRequest, TResponse> where TRequest : IAsyncRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}

public class User
{
    public static User Create(CreateUser request)
    {
        throw new System.NotImplementedException();
    }
}

// The IRequest marker interface indicates this class
// will have an associated application service handler
public class CreateUser : IRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string EmailAddress { get; set; }
    public string WebsiteUrl { get; set; }
}

public static class UserServiceModule
{
    public static void CreateUser(CreateUser request, Func<IUserRepository> userRepositoryFactory)
    {
        var userRepository = userRepositoryFactory();
        var user = User.Create(request);
        userRepository.Add(user);
        userRepository.Save();
    }
}

public class UserService : IHandle<CreateUser>
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void Handle(CreateUser request)
    {
        var user = User.Create(request);
        _userRepository.Add(user);
        _userRepository.Save();
    }
}

public class Program
{
    // This could in App_Start, Global.asax.cs, or your application's entry point
    public static void Main()
    {


        AppMediator.Initialize(app =>
        {
            app.UseDependencyResolver(new );
        });





        AppMediator.Initialize(app =>
        {
            app.Register<CreateUser>(request =>
            {
                UserServiceModule.CreateUser(request, () => new SqlUserRepository());
            });
        });
    }
}


public interface IUserRepository
{
    void Add(User user);
    void Save();
}

public class SqlUserRepository : IUserRepository
{
    public void Add(User user)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}


public class GetUser : IRequest<User>
{
    public int UserId { get; set; }
}