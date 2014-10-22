using System.Web.Http;
using AppTonic.Examples.Shared.Domain;

namespace AppTonic.Examples.Web.Controllers
{
    public class UserController : ApiController
    {
        private readonly IAppDispatcher _app;

        public UserController(IAppDispatcher app)
        {
            _app = app;
        }

        public IHttpActionResult CreateUser(CreateUser request)
        {
            _app.Handle(request);
            return Ok();
        }
    }
}

