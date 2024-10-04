using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IDonationRepository
    {
        Task<List<Donation>> GetAllDonation();
        Task<Donation> GetDonationById(string donationId);
        Task<bool> AddDonation(Donation donation);
        Task<bool> UpdateDonation(Donation donation);
        Task DeleteDonationById(string donationId);
    }
}
