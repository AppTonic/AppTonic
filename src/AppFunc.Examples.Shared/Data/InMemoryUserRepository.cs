using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppFunc.Examples.Shared.Domain;

namespace AppFunc.Examples.Shared.Data
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Save()
        {
            // not implemented 
        }

        public User GetFirst()
        {
            // Example method only, not example
            return _users.FirstOrDefault();
        }

        public Task<User> GetFirstAsync()
        {
            // Not how you would implement async at all!
            return Task.FromResult(_users.FirstOrDefault());
        }

        public void Dispose()
        {
        }
    }
}
