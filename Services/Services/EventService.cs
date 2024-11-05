using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Repository.Data.Entity;
using Repository.Data.Enum;
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

        public async Task<ServiceResponse<EventResponse>> AddEvent(EventRequest eventRequest)
        {
            try
            {
                Event _event=this._mapper.Map<Event>(eventRequest);
                _event.EventId = Guid.NewGuid().ToString();
                _event.EventStatus = EventStatus.NotStarted;
                await _unitOfWork.Events.AddEvent(_event);
                await this._unitOfWork.CommitAsync();
                

                var eventResponse = this._mapper.Map<EventResponse>(_event);

                return ServiceResponse<EventResponse>.SuccessResponseWithMessage(eventResponse);
                //return eventResponse;
            
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }

        public async Task DeleteEvent(string EventId)
        {

            Event _event;
            if (EventId != null)
            {
                _event = await _unitOfWork.Events.GetEventById(EventId);

            }
            else
            {
                throw new Exception("product Id does not exist in the system.");
            }
            _unitOfWork.Events.DeleteEvent(_event);
            await this._unitOfWork.CommitAsync();
            
        }

        
        public async Task<ServiceResponse<List<EventResponse>>> GetEvents()
        {
            try
            {
                List<Event> Event = await _unitOfWork.Events.GetEvent();
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

       

        public async Task<ServiceResponse<EventResponse>> UpdateEvent(string id, EventRequest EventRequest)
        {
            Event _event;
            if (id != null)
            {
                 _event = await _unitOfWork.Events.GetEventById(id);
                
            }
            else
            {
                throw new Exception("Event Id does not exist in the system.");
            }
            _event = _mapper.Map(EventRequest, _event);
            _unitOfWork.Events.UpdateEvent(_event);
            await this._unitOfWork.CommitAsync();

            var eventResponses = _mapper.Map<EventResponse>(_event);
            return ServiceResponse<EventResponse>.SuccessResponseWithMessage(eventResponses);

        }
    }
}
