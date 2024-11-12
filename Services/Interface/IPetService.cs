using Repository.Data.Entity;
using Repository.Data.Enum;
using Services.Models.Request;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPetService
    {
        Task<List<PetGetResponse>> GetAllPet(string searchterm);
        Task<PetGetResponse> GetPetById(string id);
        Task<string> AddPet(PetRequest petRequest);
        Task DeletePet(string id);
        Task<string> UpdatePet(string petId, PetUpdateRequest petUpdateRequest);
        Task<List<PetGetResponse>> GetAllPetByShelter(string shelterId);
    }
}
