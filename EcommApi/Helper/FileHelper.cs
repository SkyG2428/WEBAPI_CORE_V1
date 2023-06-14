using Azure.Storage.Blobs;

namespace EcommApi.Helper
{
    public static class FileHelper
    {
        public static async Task<string>UploadImage(IFormFile file)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=skyindia;AccountKey=BNTVkxbaRasLc6fk3BUi4LqOI4INGOCWPZUoUMDoyq3TaOi99bLVpqV6uYfPSb/jbtof20GWmTGU+ASt3e+IfQ==;EndpointSuffix=core.windows.net";
            string containerName = "skybookdata";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

            MemoryStream ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;
            await blobClient.UploadAsync(ms);
           return blobClient.Uri.AbsoluteUri;

            
        }

        public static async Task<string> UploadUrl(IFormFile file)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=skyindia;AccountKey=BNTVkxbaRasLc6fk3BUi4LqOI4INGOCWPZUoUMDoyq3TaOi99bLVpqV6uYfPSb/jbtof20GWmTGU+ASt3e+IfQ==;EndpointSuffix=core.windows.net";
            string containerName = "bookurl";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

            MemoryStream ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;
            await blobClient.UploadAsync(ms);
            return blobClient.Uri.AbsoluteUri;


        }
    }
}
