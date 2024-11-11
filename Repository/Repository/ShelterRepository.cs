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
    public class ShelterRepository : IShelterRepository
    {
        private readonly PawFundDbContext _dbContext;
        public ShelterRepository(PawFundDbContext _dbContext) 
        {
            this._dbContext = _dbContext;
        }

        public async Task<int> AddShelter(Shelter shelter)
        {
            await _dbContext.Shelters.AddAsync(shelter);
			var user = await _dbContext.Users
							   .FirstOrDefaultAsync(u => u.UserId == shelter.UserId);

			if (user != null) 
			{
				user.Role = Data.Enum.RoleStatus.Shelter_Staff;
			}
			return await _dbContext.SaveChangesAsync();
        }

        public async Task<Shelter> GetShelterById(string shelterId)
        {
            return await _dbContext.Shelters.FirstOrDefaultAsync(sc => sc.ShelterId.Equals(shelterId)); 
        }

        public Task<List<Shelter>> GetShelters(string? shelterId)
        {
            IQueryable<Shelter> query = _dbContext.Shelters.Include(s => s.User).Where(s => !s.IsDeleted);
            if (!string.IsNullOrEmpty(shelterId))
            {
                query = query.Where(s => s.ShelterId == shelterId); 
            }
            return query.ToListAsync();
        }

        public async Task<int> UpdateShelter(Shelter shelter)
        {
            _dbContext.Shelters.Update(shelter);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Shelter>> GetAllShelters()
        {
			IQueryable<Shelter> query = _dbContext.Shelters.Include(s => s.User).Where(s => !s.IsDeleted);
			return await query.ToListAsync();
		}

        public Task<List<Shelter>> GetShelterByUserId(string userId)
        {
            IQueryable<Shelter> query = _dbContext.Shelters.Include(s => s.User).Where(s => s.UserId == userId && !s.IsDeleted);
            return query.ToListAsync();
        }
    }
}
