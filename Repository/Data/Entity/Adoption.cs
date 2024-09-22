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
    [Table("Adoption")]
    public partial class Adoption
    {
        [Key]
        public string AdoptionId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("ShelterId")]
        public string ShelterId { get; set; }
        public Shelter Shelter { get; set; }    

        public DateTime AdoptionDate { get; set; } = DateTime.Now;
        public AdoptionStatus AdoptionStatus { get; set; }


    }
}
