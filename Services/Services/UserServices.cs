using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Repository.Models;
using Services.Helper;
using Services.Interface;
using Services.Models.Request;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserServices: IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly IEmailService _emailService;

        public UserServices(IUserRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task<ServiceResponse<User>> DeleteUser(string userId)
        {
            var user = await _repository.GetUser(userId, null, null, null);
            if (!user.Any())
            {
                return ServiceResponse<User>.ErrorResponse("User is not exist");
            }
            else
            { 
                user.First().IsDeleted = true;
                var result = await _repository.UpdateUser(user.First());
                if (result)
                {
                    return ServiceResponse<User>.SuccessResponseOnlyMessage();
                }
                else
                {
                    return ServiceResponse<User>.ErrorResponse("Delete user failed");
                }
            }
        }

        public async Task<ServiceResponse<UserResponse>> GetUser(string searchterm)
        {
            var users = await _repository.GetUser(searchterm, null, null, null);
            if (!users.Any())
            {
                return ServiceResponse<UserResponse>.ErrorResponse("User is not exist");
            }
            else
            {
                User user = users.First();
                var userResponse = new UserResponse()
                {
                    UserId = user.UserId,
                    UpdatedDate = user.UpdatedDate?.ToString("dd/MM/yyyy"),
                    Code = user.Code,
                    CreatedDate = user.CreatedDate.ToString("dd/MM/yyyy"),
                    Email = user.Email,
                    FullName = user.FullName,
                    IsDeleted = user.IsDeleted,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role.ToString(),
                    Status = user.Status,
                    Address = user.Address,
                };
                return ServiceResponse<UserResponse>.SuccessResponseWithMessage(userResponse);
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

        public async Task<ServiceResponse<User>> UpdateUser(string userId, UserRequest userRequest, string? code)
        {
            var checkUser = await _repository.GetUser(userId, null, null, null);
            if (!checkUser.Any())
            {
                return ServiceResponse<User>.ErrorResponse("User is not exist");
            }
            else
            {
                bool result = false;
                User user = checkUser.First();
                if (string.IsNullOrEmpty(code))
                {
                    var checkEmai = await _repository.GetUser(userRequest.Email, null, null, null);
                    if (checkEmai.Count() == 0)
                    {
                        string codeRandom = GenerateCode();

                        user.Status = false;
                        user.Code = codeRandom;

                        result = await _repository.UpdateUser(user);
                        if (result)
                        {
                            await _emailService.SendEmailAsync(userRequest.Email, "Confirm your account", $"Here is your code: {codeRandom}. Please enter this code to authenticate your account.");
                            return ServiceResponse<User>.SuccessResponseOnlyMessage("The system has sent the code via email. Please enter the code");
                        }
                        else
                        {
                            return ServiceResponse<User>.ErrorResponse("Update failed");
                        }
                    }
                }
                else 
                {
                    var checkCode = await _repository.GetUser(user.Email , null, null, code);
                    if (checkCode.Count() == 0)
                    {
                        return ServiceResponse<User>.ErrorResponse("Code wrong");
                    }
                    user.UserId = userId;
                    user.FullName = userRequest.FullName;
                    user.Password = userRequest.Password;
                    user.PhoneNumber = userRequest.PhoneNumber;
                    user.Email = userRequest.Email;
                    user.CreatedDate = user.CreatedDate;
                    user.Code = user.Code;
                    user.IsDeleted = user.IsDeleted;
                    user.Role = (RoleStatus)userRequest.Role;
                    user.Status = true;
                    user.UpdatedDate = DateTime.UtcNow;
                    user.Address = userRequest.Address;

                    result = await _repository.UpdateUser(user);
                    if (result)
                    {
                        return ServiceResponse<User>.SuccessResponseOnlyMessage();
                    }
                    else
                    {
                        return ServiceResponse<User>.ErrorResponse("Update failed");
                    }
                }

            }
            return null;
        }

        public async Task<ServiceResponse<List<UserResponse>>> GetAllUser()
        {
            List<UserResponse> listUser = new List<UserResponse>();
            var users = await _repository.GetUser(null, null, null, null);
            foreach (var user in users) 
            {
                var userResponse = new UserResponse()
                {
                    UserId = user.UserId,
                    UpdatedDate = user.UpdatedDate?.ToString("dd/MM/yyyy"),
                    Code = user.Code,
                    CreatedDate = user.CreatedDate.ToString("dd/MM/yyyy"),
                    Email = user.Email,
                    FullName = user.FullName,
                    IsDeleted = user.IsDeleted,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    Role = user.Role.ToString(),
                    Status = user.Status,
                    Address = user.Address,
                };
                listUser.Add(userResponse);
            }
            return ServiceResponse<List<UserResponse>>.SuccessResponseWithMessage(listUser);
        }
    }
}
