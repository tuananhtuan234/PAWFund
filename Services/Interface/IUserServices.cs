using Repository.Data.Entity;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserServices
    {
        Task<List<User>> GetAllUser(string searchterm);
        Task<User> GetUserById(string UserId);
        Task<User> Login(string email, string password);
        Task<string> AddUser(UserDTO userDTO);
        Task<string> UpdateUser(string userId, UserDTO userDTO);
        Task DeleteUser(string userId);
    }
}
