using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Models.Request;

namespace PAWFund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationServices _donationServices;

        public DonationController(IDonationServices donationServices)
        {
            _donationServices = donationServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDonation()
        {
            try
            {
                var result = await _donationServices.GetAllDonation();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetDonationById(string donationId)
        {
            try
            {
                var result = await _donationServices.GetDonationById(donationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddDonation(DonationRequest donationRequest)
        {
            try
            {
                var result = await _donationServices.AddDonation(donationRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDonation(string donationId, DonationUpdateRequest donationUpdateRequest)
        {
            try
            {
                var results = await _donationServices.UpdateDonation(donationId, donationUpdateRequest);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDonation(string donationId)
        {
            try
            {
                if (donationId == null)
                {
                    return BadRequest("Please enter your donationId");
                }
                await _donationServices.DeleteDonationById(donationId);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
