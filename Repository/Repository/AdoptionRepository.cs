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
        public Task<List<Adoption>> GetAdoption(string? adoptinId)
        {
            IQueryable<Adoption> query = _dbContext.Adoptions;
            if (!string.IsNullOrWhiteSpace(adoptinId))
            {
                query = query.Where(a => a.AdoptionId == adoptinId);    
            }
            return query.ToListAsync();

        }
    }
}
