namespace AppFunc.Examples.Shared.Domain
{
    // The IRequest marker interface indicates this class
    // will have an associated application service handler
    public class CreateUserRequest : IRequest
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string WebsiteUrl { get; set; }
    }
}