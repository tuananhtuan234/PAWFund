using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

    }
}
