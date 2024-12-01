using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entity
{
    [Table("History")]
    public partial class History
    {
        [Key]
        public string HistoryId { get; set; } = Guid.NewGuid().ToString();
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey("AdoptionId")]
        public string AdoptionId { get; set; }

        public Adoption Adoption { get; set; }

    }
}
