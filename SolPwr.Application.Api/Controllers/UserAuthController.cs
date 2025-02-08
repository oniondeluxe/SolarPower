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
        public async Task<IActionResult> RegisterAccount([FromBody] UserAccountRegistration dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _service.RegisterUserAsync(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
