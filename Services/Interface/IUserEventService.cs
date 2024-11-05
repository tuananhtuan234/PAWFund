using Services.Models.Request;
using Services.Models.Response;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Services;

namespace Services.Interface
{
    public interface IUserEventService
    {
        Task<ServiceResponse<string>> AddUserEvent(UserEventRequest userEventRequest);
        Task DeleteUserEvent(string EventId);
        Task<ServiceResponse<List<UserEventResponse>>> GetUserEvents();
    }
}
