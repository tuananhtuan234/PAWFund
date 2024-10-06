using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
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
    }
}
