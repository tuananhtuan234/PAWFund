﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Data.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly PawFundDbContext _context;

        public PetRepository(PawFundDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPet(Pet pet)
        {
            _context.Pets.Add(pet);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DeletePet(string PetId)
        {
            var pet = await GetPetById(PetId);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Pet>> GetAllPet(string searchterm)
        {
            if (searchterm != null)
            {
                return await _context.Pets.Where(sc => sc.Name.Contains(searchterm)).ToListAsync();
            }
            else
            {
                return await _context.Pets.ToListAsync();
            }
        }

		public Task<List<Pet>> GetAllPetByShelter(string shelterId)
		{
            return _context.Pets.Where(p => p.ShelterId == shelterId).ToListAsync();
		}

		public async Task<List<Pet>> GetAllPetByShelterStatus(string shelterId)
        {
            return await _context.Pets.Where(p => p.ShelterId == shelterId && p.ShelterStatus == Data.Enum.ShelterStatus.Waiting).ToListAsync();
        }

        public async Task<Pet> GetPetByAdoptionId(string adoptionId)
        {
            return await _context.Pets
                .Include(p => p.Adoption)
                .FirstOrDefaultAsync(p => p.Adoption.AdoptionId == adoptionId);
        }

        public async Task<Pet> GetPetByAdoptionIdAndShelterId(string adoptionId, string? shelterId)
        {
            return await _context.Pets
                .Include(p => p.Adoption)
                .FirstOrDefaultAsync(p => p.Adoption.AdoptionId == adoptionId && p.ShelterId == shelterId);
        }

        public async Task<Pet?> GetPetById(string PetId)
        {
            return await _context.Pets.Include(p => p.Shelter).FirstOrDefaultAsync(sc => sc.PetId.Equals(PetId));
        }

        public Task<List<Pet>> GetPetsByUserId(string userId)
        {
            var shelterId = _context.Shelters.FirstOrDefault(s => s.UserId == userId).ShelterId;
            var pets = _context.Pets.Where(p => p.ShelterId == shelterId && p.Status == Data.Enum.PetStatus.Accept).ToListAsync();
            return pets;
        }

        public async Task<bool> UpdatePet(Pet pet)
        {
            _context.Pets.Update(pet);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
