using Microsoft.AspNetCore.Http;

namespace Exercise1.DataLayer.Azure.StorageAccount
{
    public interface IBlobContainerService
    {
        public Task<string> UploadImage(IFormFile image);
        public Task<string> UpdateImage(IFormFile image, string oldImageName);
        public string GetImageUri(string imageName);
    }
}
