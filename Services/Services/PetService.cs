using Repository.Data.Entity;
using Repository.Interface;
using Services.Interface;
using Services.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;
        public PetService(IPetRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> AddPet(PetRequest petRequest)
        {
            if (petRequest == null)
            {
                return "Data not provide enough";
            }
            Pet pet = new Pet()
            {
                PetId = Guid.NewGuid().ToString(),
                ShelterId = petRequest.ShelterId,
                Name = petRequest.Name,
                Gender = petRequest.Gender,
                Ages = petRequest.Ages,
                Description = petRequest.Description,
                Species = petRequest.Species,
                Breed = petRequest.Breed,
                CreateDate = DateTime.Now,
                UpdateDate = null,
                Status = petRequest.Status,
            };
            var result = await _repository.AddPet(pet);
            return result ? "Add Successfully" : "Add Failed";
        }

        public async Task DeletePet(string id)
        {
            if(id == null)
            {
                throw new Exception("Please enter petId");
            }
            await _repository.DeletePet(id);
        }

        public Task<List<Pet>> GetAllPet(string searchterm)
        {
            return _repository.GetAllPet(searchterm);
        }

        public Task<Pet> GetPetById(string id)
        {
            return _repository.GetPetById(id);
        }

        public async Task<string> UpdatePet(string petId, PetUpdateRequest petUpdateRequest)
        {
           var existingPet = await _repository.GetPetById(petId);
            if (existingPet == null)
            {
                return "Pet not found. Please try again";
            }
            existingPet.ShelterId = petUpdateRequest.ShelterId;
            existingPet.Description = petUpdateRequest.Description;
            existingPet.Status = petUpdateRequest.Status;
            existingPet.UpdateDate = DateTime.Now;

            var result = await _repository.UpdatePet(existingPet);
            return result ? "Update Successfully" : "Update Failed";
        }
    }
}
