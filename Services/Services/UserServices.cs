using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Repository.Models;
using Repository.Repository;
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
                    var getUser = await _repository.GetUserById(userId);
                    getUser.FullName = userRequest.FullName != null ? userRequest.FullName : getUser.FullName;
                    getUser.Password = userRequest.Password != null ? userRequest.Password : getUser.Password;
                    getUser.PhoneNumber = userRequest.PhoneNumber != null ? userRequest.PhoneNumber : getUser.PhoneNumber;
                    getUser.Email = userRequest.Email != null ? userRequest.Email : getUser.Email;
                    if (userRequest.Role != null && Enum.TryParse<RoleStatus>(userRequest.Role, out var parsedRole))
                    {
                        getUser.Role = parsedRole;
                    }
                    getUser.IsDeleted = userRequest.IsDeleted;
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
                else 
                {
                    var checkCode = await _repository.GetUser(user.Email , null, null, code);
                    if (checkCode.Count() == 0)
                    {
                        return ServiceResponse<User>.ErrorResponse("Code wrong");
                    }
                    user.UserId = userId;
                    user.FullName = userRequest.FullName != null ? userRequest.FullName : user.FullName;
                    user.Password = userRequest.Password != null ? userRequest.Password : user.Password;
                    user.PhoneNumber = userRequest.PhoneNumber != null ? userRequest.PhoneNumber : user.PhoneNumber;
                    user.Email = userRequest.Email != null ? userRequest.Email : user.Email;
                    user.CreatedDate = user.CreatedDate;
                    user.Code = user.Code;
                    user.IsDeleted = user.IsDeleted;
                    if (userRequest.Role != null && Enum.TryParse<RoleStatus>(userRequest.Role, out var parsedRole))
                    {
                        user.Role = parsedRole;
                    }
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

        public async Task<User> GetUserById(string id)
        {
            return await _repository.GetUserById(id);
        }

        public async Task<ServiceResponse<PagingResult<UserResponse>>> GetUsersPaging(int currentPage, int pageSize, string search)
        {
            // Get all Users from the repository
            var allUsers = await _repository.GetUser(search, null, null, null);
            // Calculate the total number of pages
            int totalUsers = allUsers.Count;
            int totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            // Ensure the currentPage is within valid bounds
            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

            // Get the subset of Users for the current page
            var paginatedUsers = allUsers
                .Skip((currentPage - 1) * pageSize)  // Skip the number of items for previous pages
                .Take(pageSize)                      // Take the number of items for this page
                .Select(s => new UserResponse
                {
                    UserId = s.UserId,
                    Address = s.Address,
                    FullName = s.FullName,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Role = s.Role.ToString(),
                    CreatedDate = s.CreatedDate.ToString("dd/MM/yyyy"),
                    UpdatedDate = s.UpdatedDate?.ToString("dd/MM/yyyy"),
                    Code = s.Code,
                    IsDeleted = s.IsDeleted,
                    Status = s.Status
                })
                .ToList();

            // Prepare the paging result
            var pagingResult = new PagingResult<UserResponse>(
                totalUsers,    // totalItems
                totalPages,       // totalPages
                currentPage,      // currentPage
                pageSize,         // pageSize
                search,           // search
                paginatedUsers // Items
            );

            // Return the result wrapped in a ServiceResponse
            return new ServiceResponse<PagingResult<UserResponse>>
            {
                Data = pagingResult,
                Success = true,
                SuccessMessage = "Users retrieved successfully."
            };
        }
    }
}
