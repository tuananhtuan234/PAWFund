﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Request
{
    public class PetsRequest
    {
        public string PetId { get; set; }   
        public string ShelterId { get; set; }
    }
}