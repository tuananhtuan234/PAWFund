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
        Task<List<User>> GetUser(string searchterm, string email, string password, string code);
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task DeleteUser(User user);
    }
}
