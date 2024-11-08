using Repository.Data.Entity;
using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class AdoptionUserResponse
    {
        public string AdoptionId { get; set; } = Guid.NewGuid().ToString();
        public string AdoptionDate { get; set; }
        public string AdoptionStatus { get; set; }
        public ICollection<PetAdoptionResponse> Pets { get; set; } = new List<PetAdoptionResponse>();
    }
}
