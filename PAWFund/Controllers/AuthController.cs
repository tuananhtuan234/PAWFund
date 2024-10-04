using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace PAWFund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authenService;

        public AuthController(IAuthService authenService)
        {
            _authenService = authenService;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery][Required] string username, [FromQuery][Required] string password)
        {
            try
            {
                var result = await _authenService.Login(username, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //[HttpPost("Register")]
        //public Task<IActionResult> Register([FromBody][])
    }
}
