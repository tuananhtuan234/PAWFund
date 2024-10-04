using Repository.Data.Entity;
using Services.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IDonationServices
    {
        Task<List<Donation>> GetAllDonation();
        Task<Donation> GetDonationById(string donationId);
        Task<string> AddDonation(DonationRequest donationRequest);
        Task DeleteDonationById(string donationId);
        Task<string> UpdateDonation(string donationId, DonationUpdateRequest donationUpdateRequest);     
    }
}
