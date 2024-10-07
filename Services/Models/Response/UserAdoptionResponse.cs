using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class UserAdoptionResponse
    {
        public string PetId { get; set; }
        public string PetName { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }    
        public string AdoptionId {  get; set; }
        public string AdoptionDate { get; set; }
    }
}
