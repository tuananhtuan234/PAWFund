using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Models.Request;

namespace PAWFund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageServices _imageServices;

        public ImageController(IImageServices imageServices)
        {
            _imageServices = imageServices;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetImageById(string id)
        {
            try
            {
                var result = await _imageServices.GetImageById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(ImageUploadViewModel request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _imageServices.AddImage(request);
                    if (result == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Something wrong");
        }
    }
}
