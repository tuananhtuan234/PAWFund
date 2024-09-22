using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entity
{
    [Table("Donation")]
    public partial class Donation
    {
        [Key]
        public string DonationId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("ShelterId")]
        public string ShelterId { get; set; }
        public Shelter Shelter { get; set; }
        public Payment Payment { get; set; }
        public float Amount { get; set; }
        public DateTime DonationDate { get; set; } = DateTime.Now;

    }
}
