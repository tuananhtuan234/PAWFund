using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class UserAdoptionResponse
    { 
        public string AdoptionId {  get; set; }
        public string AdoptionDate { get; set; }
        public string PetId { get; set; }
        public string PetName { get; set; }
        public bool Gender { get; set; }
        public int Ages { get; set; }
        public string? Description { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string ShelterStatus { get; set; }
    }
}
