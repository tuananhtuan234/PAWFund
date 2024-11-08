using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAllPet(string searchterm);
        Task<Pet?> GetPetById(string PetId);
        Task<bool> AddPet(Pet pet);
        Task<bool> UpdatePet(Pet pet);
        Task DeletePet(string PetId);
        Task<List<Pet>> GetAllPetByShelterStatus(string shelterId);
        Task<Pet> GetPetByAdoptionId(string adoptionId);
        Task<Pet> GetPetByAdoptionIdAndShelterId(string adoptionId, string? shelterId);
        Task<List<Pet>> GetAllPetByShelter(string shelterId);
        Task<List<Pet>> GetPetsByUserId(string userId);
    }
}
