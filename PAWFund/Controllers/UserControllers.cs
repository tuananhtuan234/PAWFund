using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Models;
using Services.Interface;
using Services.Models.Request;
using Services.Models.Response;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PAWFund.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly IUserServices _services;

        public UserControllers(IUserServices services)
        {
            _services = services;
        }

        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                return Ok(await _services.GetAllUser());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser([FromQuery]string searchTerm)
        {
            try
            {
                var user = await _services.GetUser(searchTerm);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("user/id/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute]string userId)
        {
            try
            {
                var user = await _services.DeleteUser(userId);  
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("user/id/{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] UserRequest userRequest, [FromQuery] string? code)
        {
            try
            {
                var user = await _services.UpdateUser(userId, userRequest, code);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
