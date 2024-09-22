using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entity
{
    [Table("Payment")]
    public partial class Payment
    {
        [Key]
        public string PaymentId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("DonationId")]
        public string DonationId { get; set; }
        public Donation Donation { get; set; }
        public Method Method { get; set; }
        public PaymentStatus Status { get; set; }
    }
}
