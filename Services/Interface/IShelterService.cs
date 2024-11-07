using Repository.Data.Entity;
using Services.Models.Request;
using Services.Models.Response;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IShelterService
    {
        Task<ServiceResponse<ShelterRequest>> AddShelter(ShelterRequest shelterRequest);
        Task<ServiceResponse<string>> UpdateShelter(UpdateShelterRequest shelterRequest);
        Task<ServiceResponse<string>> DeleteShelter(string shelterId);
        Task<ServiceResponse<List<ShelterResponse>>> GetShelters(string? shelterId);
        Task<string> GetAllPetByShelterStatus(string shelterId, string? adoptionId, string? response, string? reason, string? emailUser, string? fullName);
        Task<ServiceResponse<List<ShelterResponse>>> GetAllShelters();
		Task<Shelter> GetShelterById(string shelterId);
        Task<ServiceResponse<PagingResult<ShelterResponse>>> GetSheltersPaging(int currentPage, int pageSize, string search);
    }
}
