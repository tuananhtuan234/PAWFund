using Services.Models.Request;
using Services.Models.Response;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IEventService
    {
        Task<ServiceResponse<EventResponse>> AddEvent(EventRequest eventRequest);
        Task<ServiceResponse<string>> UpdateEvent(EventRequest EventRequest);
        Task<ServiceResponse<string>> DeleteEvent(string EventId);
        Task<ServiceResponse<List<EventResponse>>> GetEvents(string? EventId);
    }
}
