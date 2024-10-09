using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IEventRepository
    {
        Task AddEvent(Event Event);
        Task UpdateEvent(Event Event);
        Task DeleteEvent(Event Event);
        Task<List<Event>> GetEvent();
        Task<Event> GetEventById(string? EventId);
    }
}
