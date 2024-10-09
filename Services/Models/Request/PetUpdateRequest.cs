using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class PetUpdateRequest
    {
        public string ShelterId { get; set; }
        public string? Description { get; set; }
        public PetStatus Status { get; set; }
    }
}
