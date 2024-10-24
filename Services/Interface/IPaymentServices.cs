using Microsoft.AspNetCore.Http;
using Repository.Data.Entity;
using Services.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPaymentServices
    {
        Task DeletePayment(string id);
        Task<string> AddPayment(PaymentDtos paymentRequest);
        Task<Payment> GetPaymentById(string id);
        Task<List<Payment>> GetAllPayment();
        public VnPaymentResponseModel PaymentExecute(Dictionary<string, string> url);
        public string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model, string userid);
        public string GetOrderId(string orderInfo);
        public string GetUserId(string orderInfo);
    }
}
