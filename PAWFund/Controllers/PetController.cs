using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Services.Models.Request;
using System.ComponentModel.DataAnnotations;

namespace PAWFund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPet(string? searchterm)
        {
            try
            {
                var pet = await _petService.GetAllPet(searchterm);
                return Ok(pet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetPetById(string PetId)
        {
            try
            {
                var pet = await _petService.GetPetById(PetId);
                return Ok(pet);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);   
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPPet(PetRequest petRequest)
        {
            try
            {
                var pet = await _petService.AddPet(petRequest);
                return Ok(pet);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePet(string petId, PetUpdateRequest petUpdateRequest)
        {
            try
            {
                if (petId == null)
                {
                    return BadRequest("Please enter the petId");
                }
                var pet = await _petService.UpdatePet(petId, petUpdateRequest);
                return Ok(pet);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePet(string petId)
        {
            try
            {
                await _petService.DeletePet(petId);
                return Ok("Delete Successful");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
