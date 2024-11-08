using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAdoptionRepository
    {
        Task<List<Adoption>> GetAdoption(string? adoptinId);
        Task<int> AddAdoption(Adoption adoption);
        Task<int> UpdateAdoption(Adoption adoption);
        Task<int> DeleteAdoption(Adoption adoption);
        Task<Adoption> GetAdoptionIncludeUser(string adoptinId);
        Task<List<Adoption>> GetAdoptionByUserId(string userId);
        Task<Adoption> GetUserByAdoptionId(string adoptionId);
    }
}
