using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Repository.Data.Entity
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public string UserId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber {  get; set; }
        public RoleStatus Role { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; } = DateTime.Now;
        public string Code { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status {  get; set; }   
        public Shelter Shelter { get; set; }
        public ICollection<Adoption> Adoptions { get; set; } = new List<Adoption>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
        
    }
}
