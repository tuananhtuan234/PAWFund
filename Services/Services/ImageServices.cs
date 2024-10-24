using Google.Cloud.Storage.V1;
using Repository.Data.Entity;
using Repository.Interface;
using Services.Interface;
using Services.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ImageServices: IImageServices
    {
        private readonly IImageRepository _imageRepository;
        private readonly string _projectId = "program-90fbe";
        private readonly string _bucketName = "program-90fbe.appspot.com";

        public ImageServices(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<string> AddImage(ImageUploadViewModel request)
        {
            try
            {
                if (request.UrlImage == null || request.UrlImage.Length == 0)
                {
                    return "File không hợp lệ";
                }
                if (request == null)
                {
                    return "Please enter the Image";
                }
                 using (var memoryStream = new MemoryStream())
                {
                    await request.UrlImage.CopyToAsync(memoryStream);
                    var bytes = memoryStream.ToArray();

                    // Initialize Firebase Admin SDK
                    var credential = Google.Apis.Auth.OAuth2.GoogleCredential.FromFile("PawFound.json");
                    //tạm thời hardcode, thay sau FromFile thành đường dẫn file net1701-jewelry...
                    var storage = StorageClient.Create(credential);

                    // Construct the object name (path) in Firebase Storage
                    var objectName = $"images/{DateTime.Now.Ticks}_{request.UrlImage.FileName}";

                    // Upload the file to Firebase Storage
                    var response = await storage.UploadObjectAsync(
                        bucket: _bucketName,
                        objectName: objectName,
                        contentType: request.UrlImage.ContentType,
                        source: new MemoryStream(bytes)
                    );
                    // Tải file lên Firebase Storage
                    //var storageObject = await storage.GetObjectAsync(_bucketName, objectName);
                    var downloadUrl = /*storageObject.MediaLink*/ $"https://storage.googleapis.com/{_bucketName}/{objectName}";

                    Image image = new Image()
                    {
                        ImageId = Guid.NewGuid().ToString(),
                        UrlImage = downloadUrl,
                        PetId = request.PetId,
                    };
                    var result = await _imageRepository.AddImage(image);
                    return result ? "Add Successful" : "Add Failed";
                }

        }
            catch (Exception ex)
            {
                return ex.Message;
            }
}

        public async Task<Image> GetImageById(string id)
        {
            return await _imageRepository.GetImageById(id);
        }

        public async Task DeleteImage(string id)
        {
            await _imageRepository.DeleteImage(id);
        }
    }
}
