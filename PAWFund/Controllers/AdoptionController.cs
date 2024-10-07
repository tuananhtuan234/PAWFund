using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace PAWFund.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AdoptionController : ControllerBase
    {
        private readonly IAdoptionService _adoptionService;
        public AdoptionController(IAdoptionService adoptionService)
        {
            _adoptionService = adoptionService;
        }

        [HttpGet("adoptions")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _adoptionService.GetAllAdoption(null);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("adoption")]
        public async Task<IActionResult> GetAdoptionByAdoptionId([FromQuery][Required] string adoptionId)
        {
            try
            {
                var result = await _adoptionService.GetAllAdoption(adoptionId);
                if (result.Count == 0)
                {
                    return BadRequest("Not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("adoption")]
        public async Task<IActionResult> AddAdoption([FromBody][Required] AdoptionRequest adoptionRequest)
        {
            try
            {
                var result = await _adoptionService.AddAdoption(adoptionRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("adoption/id/{id}")]
        public async Task<IActionResult> UpdateAdoption([FromBody] UpdateAdoptionRequest adoptionRequest, [FromRoute][Required] string id)
        {
            try
            {
                var result = await _adoptionService.UpdateAdoption(adoptionRequest, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("adoption/id/{id}")]
        public async Task<IActionResult> DeleteAdoption([FromRoute][Required] string id)
        {
            try
            {
                var result = await _adoptionService.DeleteAdoption(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("adoption/id/{adoptionid}")]
        public async Task<IActionResult> UpdateStatusAdoption([FromRoute] string adoptionid)
        {
            int count = 0;
            string result = null;
            result = await _adoptionService.UpdateStatusAdoption(adoptionid, count);
            if (result == "Shelter need to accept adoption" || result == "Shelter rejected can not update status")
            {
                count = -1;
            }
            if (result == "Adoption accepted")
            {
                count = 3;
            }
            if (result == "Prepare for delivery")
            {
                count = 4;
            }
            if (result == "Delivered to the driver")
            {
                count = 5;
            }
            if (result == "Adoption completed")
            {
                count = 0;
            }
            result = await _adoptionService.UpdateStatusAdoption(adoptionid, count);
            if (count == -1) 
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("adoption/id/{adoptionid}")]
        public async Task<IActionResult> FollowAdoption([FromRoute] string adoptionid,[FromQuery] string? response, [FromQuery] string? reason)
        {
            try
            {
                var adoptionStatus = await _adoptionService.FollowAdoption(adoptionid, response, reason);
                return Ok(adoptionStatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
