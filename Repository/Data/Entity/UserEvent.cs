using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Entity
{
    [Table("UserEvent")]
    public partial class UserEvent
    {
        [Key]
        public string UserEventId { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("EventId")]
        public string EventId { get; set; }
        public Event Event { get; set; }

        public DateTime RegistationDate { get; set; } = DateTime.Now;
    }
}
