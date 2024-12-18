﻿using Microsoft.EntityFrameworkCore;
using Repository.Data.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PawFundDbContext _context;

        public UserRepository(PawFundDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUser(string searchterm, string email, string password, string code)
        {
            try
            {
                IQueryable<User> query = _context.Users.Where(u => u.IsDeleted == false);
                if (!string.IsNullOrWhiteSpace(searchterm))
                {
                    query = query.Where(u => u.FullName.Contains(searchterm) || u.UserId == searchterm || u.Code == searchterm || u.Email == searchterm);
                }
                if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrEmpty(password))
                { 
                    query = query.Where(u => u.Email == email && u.Password == password);
                }
                if (!string.IsNullOrWhiteSpace(code))
                {
                    query = query.Where(u => u.Code == code);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(sc => sc.UserId.Equals(id));
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

        public async Task DeleteUser(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }    
    }
}
