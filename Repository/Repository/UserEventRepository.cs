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
    public class UserEventRepository : IUserEventRepository
    {
        private PawFundDbContext _dbContext;

        public UserEventRepository(PawFundDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task AddUserEvent(UserEvent UserEvent)
        {
            try
            {
                await this._dbContext.UserEvents.AddAsync(UserEvent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task DeleteUserEvent(UserEvent UserEvent)
        {
                try
                {
                    this._dbContext.UserEvents.Remove(UserEvent);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            
        }

        public async Task<List<UserEvent>> GetUserEvent()
        {
            return await _dbContext.UserEvents.ToListAsync();
        }

        public async Task<UserEvent> GetUserEventById(string? UserEventId)
        {
           
            try
            {
                return await _dbContext.UserEvents
                   .SingleOrDefaultAsync(p => p.UserEventId == UserEventId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
