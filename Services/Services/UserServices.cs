using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Repository.Models;
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

        public UserServices(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<User>> DeleteUser(string userId)
        {
            var user = await _repository.GetUser(userId, null, null);
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
            var users = await _repository.GetUser(searchterm, null, null);
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
                    Role = user.Role,
                    Status = user.Status
                };
                return ServiceResponse<UserResponse>.SuccessResponseWithMessage(userResponse);
            }
        }

        public async Task<ServiceResponse<User>> UpdateUser(string userId, UserRequest userRequest)
        {
            var checkUser = await _repository.GetUser(userId, null, null);
            if (!checkUser.Any())
            {
                return ServiceResponse<User>.ErrorResponse("User is not exist");
            }
            else
            {
                User user = checkUser.First();
                var updateUser = new User()
                {
                    UserId = userId,
                    FullName = userRequest.FullName,
                    Password = userRequest.Password,
                    PhoneNumber = userRequest.PhoneNumber,  
                    Email = userRequest.Email,
                    CreatedDate = user.CreatedDate,
                    Code = user.Code,
                    IsDeleted= user.IsDeleted,
                    Role = (RoleStatus)userRequest.Role,
                    Status = user.Status,
                    UpdatedDate = DateTime.UtcNow,
                };
                var result = await _repository.UpdateUser(updateUser);
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
    }
}
