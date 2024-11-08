using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Data.Enum;

namespace Services.Models.Response
{
    public class DonationResponse
    {
      
        public string DonationId { get; set; } = Guid.NewGuid().ToString();     
        public string Email { get; set; }     
        public string FullName { get; set; }       
        public string ShelterName { get; set; }
        public float Amount { get; set; }
        public DateTime DonationDate { get; set; } = DateTime.Now;
    }
}
