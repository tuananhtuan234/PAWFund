using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class UserEventRequest
    {

        public string UserId { get; set; }
        public string EventId { get; set; }
    }
}
