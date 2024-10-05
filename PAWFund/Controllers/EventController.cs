using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repository.Data.Entity;
using Repository.Interface;
using Services.Interface;
using Services.Models.Response;
using Services.Services;

namespace PAWFund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ODataController
    {
        private IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        #region Get Event
        //[EnableQuery]
        ////[PermissionAuthorize("")]
        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] string? Id)
        //{
        //    var events = await _eventService.GetEvents(Id);
        //    return Ok(events);
        //}
        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? Id)
        {
            var serviceResponse = await _eventService.GetEvents(Id);

            // Kiểm tra kết quả từ ServiceResponse và trả về danh sách EventResponse nếu thành công
            if (serviceResponse.Success)
            {
                // Chuyển đổi List<EventResponse> thành IQueryable<EventResponse> cho OData
                var events = serviceResponse.Data.AsQueryable();
                return Ok(events);
            }

            // Trả về lỗi nếu có trong ServiceResponse
            return BadRequest(serviceResponse.ErrorMessage);
        }

        #endregion

    }
}
