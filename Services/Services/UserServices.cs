using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Repository.Models;
using Services.Interface;
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

        public async Task<List<User>> GetAllUser(string searchterm)
        {
            return await _repository.GetAllUser(searchterm);
        }

        public async Task<User> GetUserById(string UserId)
        {
            return await _repository.GetUserById(UserId);
        }

        public async Task<User> Login(string email, string password)
        {
            return await _repository.Login(email, password);
        }

        public async Task<string> AddUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return "Data Empty";
            }
            var newUser = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = userDTO.Email,
                Password = userDTO.Password,
                FullName = userDTO.FullName,
                Number = userDTO.Number,
                Role = userDTO.Role,
                CreatedDate = DateTime.Now,
                UpdatedDate = null
            };
            var result = await _repository.AddUser(newUser);
            return result ? "Add Successfull" : "Add Failed";
        }

        public async Task<string> UpdateUser(string userId, UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return "Data Empty";
            }
            if (userId == null)
            {
                return "Need enter id of user";
            }
            var existingUser = await _repository.GetUserById(userId);
            existingUser.Email = userDTO.Email;
            existingUser.Password = userDTO.Password;
            existingUser.FullName = userDTO.FullName;
            existingUser.Number = userDTO.Number;
            existingUser.Role = userDTO.Role;
            existingUser.CreatedDate = DateTime.Now;
            existingUser.UpdatedDate = DateTime.Now;

            var result = await _repository.UpdateUser(existingUser);
            return result ? "Update Success" : "Update failded";
        }

        public async Task DeleteUser(string userId)
        {
            if (userId == null)
            {
                throw new Exception( "Need enter UserId");
            }
            var user = await _repository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception(" Not have this User");
            }
            else
            {
                await _repository.DeleteUser(userId);
            }

        }
    }
}
