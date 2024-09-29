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
using Services.Models.DTOs;
using Services.Models.Request;
using Services.Models.Response;
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
        private readonly AppSetting _appSettings;
        private readonly PawFundDbContext _context;
        private readonly ILogger<UserControllers> _logger;

        public UserControllers(IUserServices services, IOptionsMonitor<AppSetting> optionsMonitor, PawFundDbContext context, ILogger<UserControllers> logger)
        {
            _services = services;
            _appSettings = optionsMonitor.CurrentValue;
            _context = context;
            _logger = logger;
        }

        [HttpPost("Validate")]
        public  IActionResult Validate(LoginRequest model)
        {
            var user = _context.Users.SingleOrDefault(p => p.Email == model.Email && p.Password == model.Password);
                  
            if (user == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username//password"
                });             
            }
            // cấp token
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = GenerateToken(user)
            }) ;
        }

        private string GenerateToken(Repository.Data.Entity.User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Name", user.FullName),
                    new Claim("Email", user.Email),
                    new Claim("Password", user.Password),
                    new Claim("Role", user.Role.ToString()),
                    new Claim("TokenId", Guid.NewGuid().ToString())

                }),

                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)

            };           
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);   
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser(string searchterm)
        {
            try
            { 
                var user = await _services.GetAllUser(searchterm);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not found");
            }
            var user = await _services.AddUser(userDTO);
            if (user == null)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }
    }
}
