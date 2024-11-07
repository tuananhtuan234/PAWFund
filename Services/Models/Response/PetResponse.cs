using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class PetResponse
    {
        public string PetId { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public int Ages { get; set; }
        public string? Description { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
    }
}
