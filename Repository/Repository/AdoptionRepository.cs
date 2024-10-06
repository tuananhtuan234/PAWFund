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
    public class AdoptionRepository : IAdoptionRepository
    {
        private readonly PawFundDbContext _dbContext;

        public AdoptionRepository(PawFundDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAdoption(Adoption adoption)
        {
            await _dbContext.Adoptions.AddAsync(adoption);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAdoption(Adoption adoption)
        {
            _dbContext.Adoptions.Remove(adoption);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<List<Adoption>> GetAdoption(string? adoptinId)
        {
            IQueryable<Adoption> query = _dbContext.Adoptions;
            if (!string.IsNullOrWhiteSpace(adoptinId))
            {
                query = query.Where(a => a.AdoptionId == adoptinId);    
            }
            return query.ToListAsync();

        }

        public async Task<int> UpdateAdoption(Adoption adoption)
        {
            _dbContext.Adoptions.Update(adoption);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
