using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class DonationCustomerResponse
    {
        public string DonationId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ShelterId { get; set; }
        public string? PaymentId { get; set; }
        public float Amount { get; set; }
        public DateTime DonationDate { get; set; }
    }
}
