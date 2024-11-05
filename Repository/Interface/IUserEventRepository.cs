using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IUserEventRepository
    {
        Task<List<UserEvent>> GetUserEvent();
        Task AddUserEvent(UserEvent UserEvent);
        Task DeleteUserEvent(UserEvent UserEvent);
        Task<UserEvent> GetUserEventById(string? UserEventId);
    }
}
