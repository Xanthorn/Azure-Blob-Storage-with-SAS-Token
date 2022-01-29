using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Http;

namespace Exercise1.DataLayer.Azure.StorageAccount
{
    public class BlobContainerService : IBlobContainerService
    {
        private readonly BlobContainerClient _blobContainerClient;
        private readonly string _blobKey;

        public BlobContainerService(string blobConnectionString, string blobContainerName, string blobKey)
        {
            _blobContainerClient = new(blobConnectionString, blobContainerName);
            _blobKey = blobKey;
        }

        public async Task<string> UpdateImage(IFormFile image, string? oldImageName)
        {
            if(oldImageName == null)
            {
                BlobClient blobClient = _blobContainerClient.GetBlobClient(oldImageName);
                await blobClient.DeleteIfExistsAsync();
            }

            return await UploadImage(image);
        }

        public async Task<string> UploadImage(IFormFile image)
        {
            string uniqueName = Guid.NewGuid().ToString();

            BlobClient blobClient = _blobContainerClient.GetBlobClient(uniqueName);

            await blobClient.UploadAsync(image.OpenReadStream(),
                new BlobHttpHeaders
                {
                    ContentType = image.ContentType,
                    CacheControl = "private"
                },
                new Dictionary<string, string> {
                    { "customName", uniqueName }
                });

            return uniqueName;
        }

        public string GetImageUri(string imageName)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(imageName);

            BlobSasBuilder builder = new()
            {
                BlobContainerName = _blobContainerClient.Name,
                BlobName = blobClient.Name,
                ExpiresOn = DateTime.UtcNow.AddMinutes(5),
                Protocol = SasProtocol.Https
            };
            builder.SetPermissions(BlobSasPermissions.Read);

            UriBuilder uriBuilder = new(blobClient.Uri);
            uriBuilder.Query = builder.ToSasQueryParameters(
                new StorageSharedKeyCredential(
                    _blobContainerClient.AccountName,
                    _blobKey
                )).ToString();

            return uriBuilder.Uri.ToString();
        }
    }
}
