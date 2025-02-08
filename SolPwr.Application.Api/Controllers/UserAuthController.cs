using Microsoft.AspNetCore.Mvc;
using OnionDlx.SolPwr.Dto;
using OnionDlx.SolPwr.Services;

namespace OnionDlx.SolPwr.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserAuthController : Controller
    {
        readonly IUserAuthService _service;

        public UserAuthController(IUserAuthService service)
        {
            _service = service;
        }


        [HttpPost(Name = "RegisterAccount")]
        public Task<IActionResult> RegisterAccount([FromBody] UserAccountRegistration dto)
        {
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
