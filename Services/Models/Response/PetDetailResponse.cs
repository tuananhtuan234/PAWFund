using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class PetDetailResponse
    {
        public string PetId { get; set; }
        public string ShelterId { get; set; }
        public string UserId { get; set; }
        public string? AdoptionId { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int Ages { get; set; }
        public string? Description { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Status { get; set; }
    }
}
