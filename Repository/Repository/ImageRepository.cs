using Microsoft.EntityFrameworkCore;
using Repository.Data.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ImageRepository: IImageRepository

    {
        private readonly PawFundDbContext _context;

        public  ImageRepository(PawFundDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddImage( Image image)
        {
            _context.Images.Add(image);
           return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Image> GetImageById(string id)
        {
            return await _context.Images.FirstOrDefaultAsync(sc => sc.ImageId.Equals(id));
        }
        public async Task DeleteImage(string imageId)
        {
            Image image = await GetImageById(imageId);
            if (image != null)
            {
                _context.Images.Remove(image);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Image> GetImageByPetId(string id)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.PetId == id);
        }
    }
}
