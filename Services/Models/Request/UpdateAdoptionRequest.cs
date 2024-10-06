using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class UpdateAdoptionRequest
    {
        public int AdoptionStatus { get; set; }
        public string? Reason { get; set; }
    }
}
