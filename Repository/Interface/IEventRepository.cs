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
        Task<int> AddEvent(Event Event);
        Task<int> UpdateEvent(Event Event);
        Task<List<Event>> GetEvent(string? EventId);
    }
}
