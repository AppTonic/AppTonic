using System;

namespace AppFunc.Examples.Shared.Domain
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string WebsiteUrl { get; set; }

        public static User Create(CreateUserRequest request)
        {
            return new User
            {
                Username = request.Username,
                FullName = request.FullName,
                EmailAddress = request.EmailAddress,
                WebsiteUrl = request.WebsiteUrl
            };
        }
    }
}