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
    public class AdoptionResponse
    {
        public string AdoptionId { get; set; }
        public string UserId { get; set; }
        public string AdoptionDate { get; set; }
        public string AdoptionStatus { get; set; }
        public string? Reason { get; set; }
    }
}
