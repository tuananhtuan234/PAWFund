using Repository.Data.Entity;
using Services.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IImageServices
    {
        Task<Image> GetImageById(string id);
        Task<string> AddImage(ImageUploadViewModel request);
        Task DeleteImage(string id);
    }

}
