using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUser(string searchterm);
        Task<User> GetUserById(string UserId);
        Task<User> Login(string email, string password);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task DeleteUser(string UserId);
    }
}
