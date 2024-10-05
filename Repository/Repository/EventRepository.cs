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
    public class EventRepository : IEventRepository
    {
        private PawFundDbContext _dbContext;

        public EventRepository(PawFundDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task<int> AddEvent(Event Event)
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetEvent(string? EventId)
        {
            IQueryable<Event> query = _dbContext.Events;
            if (!string.IsNullOrEmpty(EventId))
            {
                query = query.Where(s => s.EventId == EventId);
            }
            return query.ToListAsync();
        }

        public Task<int> UpdateEvent(Event Event)
        {
            throw new NotImplementedException();
        }
    }
}
