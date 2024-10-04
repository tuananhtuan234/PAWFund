using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Services.Helper;
using Services.Interface;
using Services.Models.Request;
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
        private readonly IEmailService _emailService;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IEmailService emailService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _emailService = emailService;   
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            try
            {
                var users = await _userRepository.GetUser(null, email, password);
                User user = users.First();
                if (user == null)
                {
                    return ServiceResponse<string>.ErrorResponse("Email or password is wrong");
                }
                else
                {
                    if (!user.Status)
                    {
                        return ServiceResponse<string>.ErrorResponse("Email unverify");
                    }
                    else
                    {
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
                        return ServiceResponse<string>.SuccessResponseOnlyMessage(jwtHandler.WriteToken(jwtToken));
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private string GenerateCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            string code = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return code;
        }

        public async Task<ServiceResponse<User>> Register(UserRequest userRequest, string? code)
        {
            var checkUser = await _userRepository.GetUser(userRequest.Email, null, null);
            if (checkUser.Count != 0)
            {
                if (checkUser.First().Status)
                {
                    return ServiceResponse<User>.ErrorResponse("Email existed");
                }
            }
            if (string.IsNullOrEmpty(code))
            {
                string codeRandom = GenerateCode();
                var user = new User()
                {
                    UserId = Guid.NewGuid().ToString(),
                    Email = userRequest.Email,
                    Code = codeRandom,
                    CreatedDate = DateTime.Now,
                    PhoneNumber = userRequest.PhoneNumber,
                    FullName = userRequest.FullName,
                    IsDeleted = false,
                    Status = false,
                    Password = userRequest.Password,
                    Role = Enum.Parse<RoleStatus>(userRequest.Role.ToString()),
                    UpdatedDate = null,
                };
                var result = await _userRepository.AddUser(user);
                if (result)
                {
                    await _emailService.SendEmailAsync(userRequest.Email, "Confirm your account", $"Here is your code: {codeRandom}. Please enter this code to authenticate your account.");
                    return ServiceResponse<User>.SuccessResponseOnlyMessage("The system has sent the code via email. Please enter the code");
                }
                else
                {
                    return ServiceResponse<User>.ErrorResponse("Add user failed");
                }
            }
            else
            {
                var checkCode = await _userRepository.GetUser(code, null, null);
                if (!checkCode.Any())
                {
                    return ServiceResponse<User>.ErrorResponse("Code wrong");
                }
                else
                {
                    checkCode.First().Status = true;
                    var result = await _userRepository.UpdateUser(checkCode.First());
                    if (result)
                    {
                        return ServiceResponse<User>.SuccessResponse(checkCode.First());
                    }
                    else
                    {
                        return ServiceResponse<User>.ErrorResponse("Add user failed");
                    }
                }
            }
        }
    }
}
