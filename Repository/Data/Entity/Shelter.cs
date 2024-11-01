﻿using Repository.Data.Enum;
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
    [Table("Shelter")]
    public partial class Shelter
    {
        [Key]
        [Required]
        public string ShelterId { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string ShelterName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Description { get; set; }
        public DateTime ShelterDate { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<Event> Events { get; set; } = new List<Event>();

    }
}
