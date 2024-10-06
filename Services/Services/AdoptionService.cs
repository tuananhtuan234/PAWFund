using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Services.Interface;
using Services.Models.Request;
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

        public async Task<string> AddAdoption(AdoptionRequest adoptionRequest)
        {
            Adoption adoption = new Adoption()
            {
                AdoptionDate = DateTime.Now,
                AdoptionId = Guid.NewGuid().ToString(),
                AdoptionStatus = AdoptionStatus.Pending,
                UserId = adoptionRequest.UserId, 
                Reason = null,
            };
            int result = await _adoptionRepository.AddAdoption(adoption);
            if (result == 0)
            {
                return "Add adoption failed";
            }
            else
            {
                return "Add adoption success";
            }
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

        public Task<string> UpdateAdoption(AdoptionRequest adoption)
        {
            throw new NotImplementedException();
        }
    }
}
