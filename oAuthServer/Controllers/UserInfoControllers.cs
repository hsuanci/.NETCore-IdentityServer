using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auth.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserInfoController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public string GetInfo()
        {
            return "Hey Henry";
        }
    }
}
