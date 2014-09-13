using System;

namespace AppFunc.Examples.Shared.Domain
{
    public class GetUserRequest : IRequest<User>
    {
        public Guid UserId { get; set; }
    }
}
