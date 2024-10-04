using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Models.Request;
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
        public async Task<IActionResult> Login([FromQuery][Required] string email, [FromQuery][Required] string password)
        {
            try
            {
                var result = await _authenService.Login(email, password);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody][Required] UserRequest userRequest, [FromQuery] string? code)
        {
            try
            {
                return Ok(await _authenService.Register(userRequest, code));
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);    
            }
        }
    }
}
