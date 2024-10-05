using Repository.Data.Entity;
using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class EventRequest
    {

        [Required]
        public string EventId { get; set; }

        [Required]
        public string ShelterId { get; set; }

        [Required]
        public Shelter Shelter { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string EventDescription { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public EventStatus EventStatus { get; set; }
    }
}
