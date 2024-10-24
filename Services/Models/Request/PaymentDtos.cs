using Repository.Data.Entity;
using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class PaymentDtos
    {
      
        public string PaymentId { get; set; }       
        public string DonationId { get; set; }
     
        public Method Method { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
