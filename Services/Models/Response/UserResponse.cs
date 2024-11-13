using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class UserResponse
    {
        public string UserId { get; set; } 
        public string Email { get; set; }
        public string? Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
    }
}
