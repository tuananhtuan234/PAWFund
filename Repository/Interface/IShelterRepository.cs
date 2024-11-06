using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IShelterRepository
    {
        Task<int> AddShelter(Shelter shelter);
        Task<int> UpdateShelter(Shelter shelter);
        Task<List<Shelter>> GetShelters(string? shelterId);
        Task<List<Shelter>> GetAllShelters();
        Task<Shelter> GetShelterById(string shelterId);


    }
}
