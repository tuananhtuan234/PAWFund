using Repository.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Repository.Data.Entity
{
    [Table("Event")]
    public partial class Event
    {
        [Key]
        public string EventId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("ShelterId")]
        public string ShelterId { get; set; }
        public Shelter Shelter { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime DateEnd { get; set; }
        public EventStatus EventStatus { get; set; }

        public  ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();

    }
}
