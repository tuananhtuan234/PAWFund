using Microsoft.AspNetCore.Http;
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
    }
}
