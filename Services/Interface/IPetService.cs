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
        Task<List<Pet>> GetAllPet(string searchterm);
        Task<Pet> GetPetById(string id);
        Task<string> AddPet(PetRequest petRequest);
        Task DeletePet(string id);
        Task<string> UpdatePet(string petId, PetUpdateRequest petUpdateRequest);
        Task<List<Pet>> GetAllPetByShelter(string shelterId);
    }
}
