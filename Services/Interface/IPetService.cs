using Repository.Data.Entity;
using Services.Models.Request;
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
    }
}
