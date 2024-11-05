using AutoMapper;
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
    public class UserEventService : IUserEventService
    {
        private readonly UnitOfWork _unitOfWork;
        private IMapper _mapper;
        public UserEventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<string>> AddUserEvent(UserEventRequest userEventRequest)
        {
            try
            {
                List<UserEvent> listUserEvent = await _unitOfWork.UserEvents.GetUserEvent();
                bool userEventExists = listUserEvent.Any(ue => ue.UserId == userEventRequest.UserId && ue.EventId == userEventRequest.EventId);
                if (userEventExists)
                {
                    return ServiceResponse<string>.ErrorResponse("Bạn đã tham gia event này rồi.");
                }
                var UserEvent = new UserEvent()
                {
                    UserEventId = Guid.NewGuid().ToString(),
                    UserId = userEventRequest.UserId,
                    EventId = userEventRequest.EventId,
                    RegistationDate = DateTime.Now,

                };
                await _unitOfWork.UserEvents.AddUserEvent(UserEvent);
                await this._unitOfWork.CommitAsync();



                return ServiceResponse<string>.SuccessResponseWithMessage("Tham gia event thành công");
              

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task DeleteUserEvent(string UserEventId)
        {
            UserEvent userEvent;
            if (UserEventId != null)
            {
                userEvent = await _unitOfWork.UserEvents.GetUserEventById(UserEventId);

            }
            else
            {
                throw new Exception("product Id does not exist in the system.");
            }
            _unitOfWork.UserEvents.DeleteUserEvent(userEvent);
            await this._unitOfWork.CommitAsync();
        }




        public async Task<ServiceResponse<List<UserEventResponse>>> GetUserEvents()
        {
            try
            {
                List<UserEvent> UserEvent = await _unitOfWork.UserEvents.GetUserEvent();
                if (UserEvent == null)
                {
                    throw new Exception("AccountId does not exist in system");
                }
                var userEventResponses = _mapper.Map<List<UserEventResponse>>(UserEvent);
                return ServiceResponse<List<UserEventResponse>>.SuccessResponseWithMessage(userEventResponses);


            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
