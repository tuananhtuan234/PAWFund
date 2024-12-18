﻿using Repository.Data.Entity;
using Repository.Interface;
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
    public class DonationServices : IDonationServices
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShelterRepository _shelterRepository;
        private readonly IPaymentRepository _paymentRepository;

        public DonationServices(IDonationRepository donationRepository, IPaymentRepository paymentRepository, IUserRepository userRepository, IShelterRepository shelterRepository)
        {
            _donationRepository = donationRepository;
            _userRepository = userRepository;
            _shelterRepository = shelterRepository;
            _paymentRepository = paymentRepository;
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

        public async Task<List<DonationResponse>> GetAllDonation()
        {
            List<DonationResponse> donationResponses = new List<DonationResponse>();
            var listDonation = await _donationRepository.GetAllDonation();
            foreach (var item in listDonation)
            {
                var donation = await _donationRepository.GetDonationById(item.DonationId);
                var user = await _userRepository.GetUserById(item.UserId);
                var shelter = await _shelterRepository.GetShelterById(item.ShelterId);
                
                var newDonation = new DonationResponse()
                {
                    DonationId = item.DonationId,                  
                    Email = user.Email,
                    FullName = user.FullName,                  
                    ShelterName = shelter.ShelterName,                  
                    Amount = item.Amount,
                    DonationDate = item.DonationDate,
                };
                donationResponses.Add(newDonation);
            }
            return donationResponses;
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

        public async Task<List<DonationCustomerResponse>> GetListDonationbyShelterId(string shelterId)
        {
            var donation = await _donationRepository.GetListDonationbyShelterId(shelterId);
            List<DonationCustomerResponse> donationCustomerResponses = new List<DonationCustomerResponse>();
            DonationCustomerResponse response = new DonationCustomerResponse();
            foreach (var item in donation)
            {
                response.DonationId = item.DonationId;
                response.Amount = item.Amount;
                response.DonationDate = item.DonationDate;
                response.UserName = item.User.FullName;
                response.UserId = item.UserId;
                response.ShelterId = item.ShelterId;
                response.PaymentId = item.Payment != null ? item.Payment.PaymentId : null;
                donationCustomerResponses.Add(response);
            }
            return donationCustomerResponses;
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
