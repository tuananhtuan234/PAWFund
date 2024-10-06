using Repository.Data.Enum;
using Repository.Interface;
using Services.Interface;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AdoptionService : IAdoptionService
    {
        private readonly IAdoptionRepository _adoptionRepository;
        public AdoptionService(IAdoptionRepository adoptionRepository) 
        {
            _adoptionRepository = adoptionRepository;
        }

        public async Task<List<AdoptionResponse>> GetAllAdoption(string? adoptionId)
        {
            List<AdoptionResponse> result = new List<AdoptionResponse>();
            var adoptions = await _adoptionRepository.GetAdoption(adoptionId);
            foreach (var adoption in adoptions) 
            {
                AdoptionResponse adoptionResponse = new AdoptionResponse()
                {
                    AdoptionId = adoption.AdoptionId,
                    AdoptionDate = adoption.AdoptionDate.ToString("dd/MM/yyyy"),
                    AdoptionStatus = adoption.AdoptionStatus.ToString(),
                    Reason = adoption.Reason,
                    UserId = adoption.UserId,
                };
                result.Add(adoptionResponse);
            }
            return result;
        }
    }
}
