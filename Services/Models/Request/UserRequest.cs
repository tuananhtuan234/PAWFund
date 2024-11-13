using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class UserRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; } 
        [Required]
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
    }
}
