using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            try
            {
                var user = await _userRepository.Login(username, password);
                if (user == null)
                {
                    return ServiceResponse<string>.ErrorResponse("Username or password is wrong");
                }
                var jwtHandler = new JwtSecurityTokenHandler();
                var issuer = _configuration["AppSettings:Issuer"];
                var audience = _configuration["AppSettings:Audience"];

                var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:SecretKey"]);
                var tokenDes = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", user.UserId),
                        new Claim("Email", user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = issuer,
                    Audience = audience, // Đảm bảo rằng giá trị này được lấy từ cấu hình
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                var jwtToken = jwtHandler.CreateToken(tokenDes);
                return ServiceResponse<string>.SuccessResponse(jwtHandler.WriteToken(jwtToken));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
