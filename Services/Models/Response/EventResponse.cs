using Repository.Data.Entity;
using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Response
{
    public class EventResponse
    {
        [Key]
        public string EventId { get; set; } 

        
        public string ShelterId { get; set; }
        public Shelter Shelter { get; set; }

        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public EventStatus EventStatus { get; set; }
    }
}
