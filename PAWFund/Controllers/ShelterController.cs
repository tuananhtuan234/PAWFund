﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using Services.Models.Request;
using Services.Models.Response;
using Services.Services;

namespace PAWFund.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterController : ControllerBase
    {
        private readonly IShelterService shelterService;

        public ShelterController(IShelterService shelterService)
        {
            this.shelterService = shelterService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddShelter([FromBody]ShelterRequest shelterRequest)
        {
            var shelter = await shelterService.AddShelter(shelterRequest);
            return Ok(shelter);
        }

        [HttpGet]

        public async Task<IActionResult> GetShelters([FromQuery] string? shelterId)
        {
            var shelter = await shelterService.GetShelters(shelterId);
            return Ok(shelter);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteShelter([FromQuery]string shelterId)
        {
            var result = await shelterService.DeleteShelter(shelterId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShelter([FromBody] UpdateShelterRequest shelter)
        {
            var result = await shelterService.UpdateShelter(shelter);
            return Ok(result);
        }

        [HttpGet("pets/shelterid/{shelterid}")]
        public async Task<IActionResult> GetPetByShelterStatus([FromRoute] string shelterid, [FromQuery] string? adoptionId, [FromQuery] string? response, [FromQuery] string? reason, [FromQuery] string? emailUser, [FromQuery] string? fullName)
        {
            try
            {
                var result = await shelterService.GetAllPetByShelterStatus(shelterid, adoptionId, response, reason, emailUser, fullName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
