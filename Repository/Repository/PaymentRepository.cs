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
    public  class PaymentRepository: IPaymentRepository
    {
        private readonly PawFundDbContext _context;

        public PaymentRepository(PawFundDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetAllPayment()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetPaymentByDonationId(string donationId)
        {
            return await _context.Payments.FirstOrDefaultAsync(sc => sc.DonationId.Equals(donationId));
        }

        public async Task<Payment> GetPaymentById(string id)
        {
            return await _context.Payments.FirstOrDefaultAsync(sc => sc.PaymentId.Equals(id));
        }

        public async Task<bool> AddPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DeletePayment(string id)
        {
            var payment = await GetPaymentById(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                 await _context.SaveChangesAsync();
            }
        }
    }
}
