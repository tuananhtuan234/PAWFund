using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class AceptPetRequest
    {
        public string PetId { get; set; }
        public string Respone { get; set; }
        public string? Reason { get; set; }

    }
}
