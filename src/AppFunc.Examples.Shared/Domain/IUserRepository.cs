using System.Threading.Tasks;

namespace AppFunc.Examples.Shared.Domain
{
    public interface IUserRepository
    {
        void Add(User user);
        void Save();
        User GetFirst();
        Task<User> GetFirstAsync();
    }
}