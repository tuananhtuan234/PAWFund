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
    public class UpdateShelterRequest
    {
        [Required]
        public string ShelterId { get; set; } = Guid.NewGuid().ToString();
        public string? UserId { get; set; }
        public string? ShelterName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? ShelterDate { get; set; }
    }
}
