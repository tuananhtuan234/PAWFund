using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class ShelterRequest
    {
        [Required]
        public string ShelterName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
