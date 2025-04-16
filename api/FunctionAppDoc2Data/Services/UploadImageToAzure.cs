using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Services;
public static class UploadImageToAzure
{
    public async static Task<string> UploadImage(IFormFile formFile)
    {
        string connectionString = Environment.GetEnvironmentVariable("AZURESTORAGE_CONNECTION_STRING");
        string containerName = Environment.GetEnvironmentVariable("AZURESTORAGE_CONTAINER_NAME");
        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

        using (Stream myBlob = formFile.OpenReadStream())
        {
            var blobClient = new BlobContainerClient(connectionString, containerName);
            var blob = blobClient.GetBlobClient(uniqueFileName);
            await blob.UploadAsync(myBlob, overwrite: true);
        }
        return uniqueFileName;
    }

    public static string DownloadImage(string imagePath)
    {
        if (String.IsNullOrEmpty(imagePath))
        {
            return string.Empty;
        }
        try
        {
            string connectionString = Environment.GetEnvironmentVariable("AZURESTORAGE_CONNECTION_STRING");
            string containerName = Environment.GetEnvironmentVariable("AZURESTORAGE_CONTAINER_NAME");

            var blobClient = new BlobContainerClient(connectionString, containerName);
            var blob = blobClient.GetBlobClient(imagePath);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                blob.DownloadTo(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                string fileExtension = Path.GetExtension(imagePath).ToLower();
                if (fileExtension == ".pdf")
                {
                    return ConvertPdfToBase64(imageBytes);
                }
                else if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
                {
                    return "data:image/" + fileExtension.Substring(1) + ";base64," + Convert.ToBase64String(imageBytes);
                }
                else
                {
                    return string.Empty; // Unsupported file type
                }
            }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    private static string ConvertPdfToBase64(byte[] pdfBytes)
    {
        using (var ms = new MemoryStream(pdfBytes))
        {
            var imageBytes = ConvertPdfPageToImage(ms);
            return "data:image/png;base64," + Convert.ToBase64String(imageBytes);
        }
    }

    private static byte[] ConvertPdfPageToImage(MemoryStream pdfStream)
    {
        return new byte[] { };
    }
}
