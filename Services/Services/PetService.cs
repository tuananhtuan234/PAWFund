using Repository.Data.Entity;
using Repository.Data.Enum;
using Repository.Interface;
using Services.Helper;
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
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;
        private readonly IAdoptionRepository _adoptionRepository;
        private readonly IUserRepository _userRepository;   
        private readonly IEmailService _emailService;
        private readonly IShelterRepository _shelterRepository;
        public PetService(IPetRepository repository, IAdoptionRepository adoptionRepository, IUserRepository userRepository, IEmailService emailService, IShelterRepository shelterRepository)
        {
            _repository = repository;
            _adoptionRepository = adoptionRepository;   
            _userRepository = userRepository;
            _emailService = emailService;
            _shelterRepository = shelterRepository;
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
                AdoptionId = null,
                ShelterStatus = null,
                Reason = null,
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

		public async Task<List<Pet>> GetAllPetByShelter(string shelterId)
		{
            return await _repository.GetAllPetByShelter(shelterId);
		}

		public async Task<PetDetailResponse> GetPetById(string id)
        {
            var pet = await _repository.GetPetById(id);
            if (pet == null)
            {
                return null;
            }
            var petResponse = new PetDetailResponse()
            {
                PetId = pet.PetId,
                ShelterId = pet.ShelterId,
                UserId = pet.Shelter.UserId,
                AdoptionId = pet.AdoptionId,
                Name = pet.Name,
                Description = pet.Description,
                Species = pet.Species,
                Breed = pet.Breed,
                Status = pet.Status.ToString(),
            };
            return petResponse;
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
