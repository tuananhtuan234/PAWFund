using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPayment();
        Task<Payment> GetPaymentById(string id);
        Task<bool> AddPayment(Payment payment);
        Task DeletePayment(string id);
    }
}
