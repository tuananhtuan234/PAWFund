using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Services.Models.Request
{
    public class ImageUploadViewModel
    {                 
        public IFormFile UrlImage { get; set; }     
        public string PetId { get; set; }       
    }
}
