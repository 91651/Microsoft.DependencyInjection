using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return _user.GetAllUser();
        }
    }
}
