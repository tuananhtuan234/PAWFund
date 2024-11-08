using Repository.Data.Entity;
using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class PetAdoptionResponse
    {
        public string PetId { get; set; } = Guid.NewGuid().ToString();
        public string ShelterName { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int Ages { get; set; }
        public string? Description { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Status { get; set; }
        public ICollection<Image> Images { get; set; } = new List<Image>();
        public string? ShelterStatus { get; set; }
        public string? Reason { get; set; }
    }
}
