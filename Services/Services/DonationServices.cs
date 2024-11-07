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
    public class DonationServices : IDonationServices
    {
        private readonly IDonationRepository _donationRepository;

        public DonationServices(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<string> AddDonation(DonationRequest donationRequest)
        {
           if (donationRequest == null)
            {
                return "Data not null";
            }
           Donation donation = new Donation()
           {
               DonationId = donationRequest.DonationId,
               ShelterId = donationRequest.ShelterId,
               UserId = donationRequest.UserId,
               Amount = donationRequest.Amount,
               DonationDate = DateTime.Now,
           };
            var result = await _donationRepository.AddDonation(donation);
            return result ? "Add Successfully" : "Add Failed";
        }

        public async Task DeleteDonationById(string donationId)
        {
            try
            {
                if (donationId == null)
                {
                    throw new Exception("Please enter your donationId");
                }
                await _donationRepository.DeleteDonationById(donationId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Donation>> GetAllDonation()
        {
            return await _donationRepository.GetAllDonation();
        }

        public async Task<Donation> GetDonationById(string donationId)
        {
            try
            {
                if (donationId == null)
                {
                    throw new Exception("Please enter your donationId");
                }
                return await _donationRepository.GetDonationById(donationId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Donation>> GetListDonationbyUserId(string userId)
        {
           return await _donationRepository.GetListDonationbyUserId(userId);
        }

        public async Task<string> UpdateDonation(string donationId, DonationUpdateRequest donationUpdateRequest)
        {
            var existingDonation = await _donationRepository.GetDonationById(donationId);
            if (existingDonation == null)
            {
                return "donation not found";
            }
            if (donationUpdateRequest == null)
            {
                return "data is null";
            }
            existingDonation.Amount = donationUpdateRequest.Amount;
            existingDonation.DonationDate = DateTime.Now;
            var result = await _donationRepository.UpdateDonation(existingDonation);
            return result ? "Update Successfully" : "Update failed";
        }
    }
}
