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
    [Route("api/[controller]")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly IUserServices _services;

        public UserControllers(IUserServices services)
        {
            _services = services;
        }

        [HttpGet("GetAllUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                return Ok(await _services.GetUser(null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser")]
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

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromQuery][Required] string userId)
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

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromQuery][Required] string userId, [FromBody] UserRequest userRequest)
        {
            try
            {
                var user = await _services.UpdateUser(userId, userRequest);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
