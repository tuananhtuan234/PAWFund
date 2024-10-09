using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository.Data.Entity;
using Repository.Interface;
using Services.Interface;
using Services.Models.Request;
using Services.Models.Response;
using Services.Services;

namespace PAWFund.Controllers
{
    
    public class EventController : ODataController
    {
        private IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        #region Get Event

        [EnableQuery]
        [HttpGet("odata/Event")]
        public async Task<IActionResult>  GetAllEvents()
        {
            // Trả về tất cả sự kiện
            var serviceResponse = await _eventService.GetEvents();

            if (serviceResponse.Success)
            {
                var events = serviceResponse.Data.AsQueryable();
                return Ok(events);
            }

            return BadRequest(serviceResponse.ErrorMessage);
        }
        #endregion

        #region Create Event
        [EnableQuery]
        [HttpPost("odata/Event")]
        public async Task<IActionResult> Post([FromForm] EventRequest eventRequest)
        {

            ServiceResponse<EventResponse> product = await this._eventService.AddEvent(eventRequest);
            return Created($"odata/Events/{product.Data.EventId}", product);
        }
        #endregion
        #region Update Event
        [EnableQuery]
        [HttpPut("odata/Event/{key}")]
        public async Task<IActionResult> Put([FromRoute] string key, [FromForm] EventRequest eventRequest)
        {
            ServiceResponse<EventResponse> product = await this._eventService.UpdateEvent(key, eventRequest);
            return Updated(product);
        }
        #endregion

        #region Delete Event
        [EnableQuery]
        [HttpDelete("odata/Event/{key}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] string key)
        {
            await this._eventService.DeleteEvent(key);
            return NoContent();
        }
        #endregion
    }
}
