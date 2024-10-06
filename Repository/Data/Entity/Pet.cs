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
    [Table("Pet")]
    public partial class Pet
    {
        [Key]
        public string PetId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("ShelterId")]
        public string ShelterId { get; set; }
        public Shelter Shelter { get; set; }

        [Required]
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int Ages { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Species { get; set; }
        [Required]
        public string Breed { get; set; }
        public string? AdoptionId { get; set; }
        public Adoption? Adoption { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; } = DateTime.Now;
        public PetStatus Status { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
