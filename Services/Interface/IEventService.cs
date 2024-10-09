using Services.Models.Request;
using Services.Models.Response;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.Interface
{
    public interface IEventService
    {
        Task<ServiceResponse<EventResponse>> AddEvent(EventRequest eventRequest);
        Task<ServiceResponse<EventResponse>> UpdateEvent(string id,EventRequest EventRequest);
        Task DeleteEvent(string EventId);
        Task<ServiceResponse<List<EventResponse>>> GetEvents();
    }
}
