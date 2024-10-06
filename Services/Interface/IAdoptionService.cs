using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Data.Entity;
using Services.Models.Request;
using Services.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IAdoptionService
    {
        Task<List<AdoptionResponse>> GetAllAdoption(string? adoptionId);
        Task<string> AddAdoption(AdoptionRequest adoption);
        Task<string> UpdateAdoption(UpdateAdoptionRequest adoption, string id);
        Task<string> DeleteAdoption(AdoptionRequest adoption);
    }
}
