using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services.Interface;
using Services.Models.Request;
using Services.Models.Response;
using Services.Services;

namespace PAWFund.Controllers
{

    public class UserEventController : ODataController
    {
        private IUserEventService _userEventService;
        public UserEventController(IUserEventService userEventService)
        {
            _userEventService = userEventService;
        }
        #region Get Event

        [EnableQuery]
        [HttpGet("odata/UserEvent")]
        public async Task<IActionResult> GetAllUserEvents()
        {
            // Trả về tất cả sự kiện
            var serviceResponse = await _userEventService.GetUserEvents();

            if (serviceResponse.Success)
            {
                var Userevents = serviceResponse.Data.AsQueryable();
                return Ok(Userevents);
            }

            return BadRequest(serviceResponse.ErrorMessage);
        }
        #endregion
        #region Create UserEvent
        [EnableQuery]
        [HttpPost("odata/UserEvent")]
        public async Task<IActionResult> Post([FromForm] UserEventRequest userEventRequest)
        {

            ServiceResponse<string> UserEvent = await this._userEventService.AddUserEvent(userEventRequest);
            
            
                return Created("odata/UserEvent", UserEvent);
           
                
            


        }
        #endregion
        #region Delete UserEvent
        [EnableQuery]
        [HttpDelete("odata/UserEvent/{key}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] string key)
        {
            await this._userEventService.DeleteUserEvent(key);
            return NoContent();
        }
        #endregion
    }

}
