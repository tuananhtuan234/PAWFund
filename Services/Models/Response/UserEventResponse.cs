using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class UserEventResponse
    {
        [Key]
        public string UserEventId { get; set; } 

       
        public string UserId { get; set; }

        public string EventId { get; set; }

        public DateTime RegistationDate { get; set; }
    }
}
