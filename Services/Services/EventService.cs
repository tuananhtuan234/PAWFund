using AutoMapper;
using Microsoft.Identity.Client;
using Repository.Data.Entity;
using Repository.Interface;
using Repository.Repository;
using Services.Interface;
using Services.Models.Request;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class EventService : IEventService
    {
        private readonly UnitOfWork _unitOfWork;
        private IMapper _mapper;
        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
        }

        public Task<ServiceResponse<EventResponse>> AddEvent(EventRequest eventRequest)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> DeleteEvent(string EventId)
        {
            throw new NotImplementedException();
        }

        
        public async Task<ServiceResponse<List<EventResponse>>> GetEvents(string Id)
        {
            try
            {
                List<Event> Event = await _unitOfWork.Events.GetEvent(Id);
                if (Event == null)
                {
                    throw new Exception("AccountId does not exist in system");
                }
                var eventResponses = _mapper.Map<List<EventResponse>>(Event);
                return ServiceResponse<List<EventResponse>>.SuccessResponseWithMessage(eventResponses);


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       

        public Task<ServiceResponse<string>> UpdateEvent(EventRequest EventRequest)
        {
            throw new NotImplementedException();
        }
    }
}
