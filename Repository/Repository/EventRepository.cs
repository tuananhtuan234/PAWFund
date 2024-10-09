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

        public async Task AddEvent(Event Event)
        {
            try
            {
                await this._dbContext.Events.AddAsync(Event);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task DeleteEvent(Event Event)
        {
            try
            {
                this._dbContext.Events.Remove(Event);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Event>> GetEvent()
        {
            //IQueryable<Event> query = _dbContext.Events;
            //if (!string.IsNullOrEmpty(EventId))
            //{
            //    query = query.Where(s => s.EventId == EventId);
            //}
            return await _dbContext.Events.ToListAsync();
        }
        public async Task<Event> GetEventById(string EventId)
        {
            //return _dbContext.Events.Where(s => s.EventId == EventId).SingleOrDefaultAsync();
            try
            {
                return await _dbContext.Events
                   .SingleOrDefaultAsync(p => p.EventId == EventId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateEvent(Event Event)
        {
            try
            {
                this._dbContext.Entry<Event>(Event).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
