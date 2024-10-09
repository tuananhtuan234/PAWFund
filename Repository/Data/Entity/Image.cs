using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entity
{
    [Table("Image")]
    public partial class Image
    {
        [Key]
        public string ImageId { get; set; }
        [Required]
        public string UrlImage { get; set; }
        [ForeignKey("PetId")]
        public string PetId { get; set; }
        public Pet Pet { get; set; }

    }
}
