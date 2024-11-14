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
    public interface IDonationServices
    {
        Task<List<DonationResponse>> GetAllDonation();
        Task<Donation> GetDonationById(string donationId);
        Task<List<Donation>> GetListDonationbyUserId(string userId);
        Task<List<DonationCustomerResponse>> GetListDonationbyShelterId(string shelterId);
        Task<string> AddDonation(DonationRequest donationRequest);
        Task DeleteDonationById(string donationId);
        Task<string> UpdateDonation(string donationId, DonationUpdateRequest donationUpdateRequest);     
    }
}
