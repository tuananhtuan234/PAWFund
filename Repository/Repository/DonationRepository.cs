using Microsoft.EntityFrameworkCore;
using Repository.Data.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class DonationRepository: IDonationRepository
    {
        private readonly PawFundDbContext _context;

        public DonationRepository(PawFundDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddDonation(Donation donation)
        {
            _context.Donations.Add(donation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DeleteDonationById(string donationId)
        {
            var donation = await GetDonationById(donationId);
            if(donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Donation>> GetAllDonation()
        {
           return await _context.Donations.ToListAsync();
        }

        public async Task<Donation> GetDonationById(string donationId)
        {
           return await _context.Donations.FirstOrDefaultAsync(sc => sc.DonationId.Equals(donationId));
        }

        public async Task<bool> UpdateDonation(Donation donation)
        {
            _context.Donations.Update(donation);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
