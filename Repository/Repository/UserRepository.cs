using Microsoft.EntityFrameworkCore;
using Repository.Data.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly PawFundDbContext _context;

        public UserRepository(PawFundDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUser(string searchterm)
        {
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchterm))
            {
                query = query.Where(sc => sc.FullName.Contains(searchterm) || sc.Email.Contains(searchterm));
            }
            var user = await query.ToListAsync();
            return user;
        }

        public async Task<User> GetUserById(string UserId)
        {
            return await _context.Users.FirstOrDefaultAsync(sc => sc.UserId.Equals(UserId));
        }

        public async Task<User> Login(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(sc => sc.Email.Equals(email) && sc.Password.Equals(password));
        }
        public async Task<bool> AddUser(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DeleteUser(string UserId)
        {
            User user = await GetUserById(UserId);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
