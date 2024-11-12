using Repository.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IImageRepository
    {
        Task DeleteImage(string imageId);
        Task<Image> GetImageById(string id);
        Task<bool> AddImage(Image image);
        Task<Image> GetImageByPetId(string id);
    }
}
