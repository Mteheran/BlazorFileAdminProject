using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using shared;

namespace api.Services
{
   public class AzureStorageService : IAzureStorageService
   {       
        private readonly IConfiguration Configuration;
        private readonly  string containerName = "blazorfiles";
        private string connectionString = "";
        public AzureStorageService(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString =  Configuration["AZURE_STORAGE_CONNECTION_STRING1"];
        }
        public async Task SaveFileAsync(FileData file)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);    

            // Create the container and return a container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName); 

            await containerClient.UploadBlobAsync(file.FileName, new MemoryStream(file.FileInfo));
        }

        public async Task<IEnumerable<FileData>> GetFiles()
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            // Create the container and return a container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName); 

            var blobs = containerClient.GetBlobs(Azure.Storage.Blobs.Models.BlobTraits.All, Azure.Storage.Blobs.Models.BlobStates.Version);
            List<FileData> list = new List<FileData>();

            foreach(var item in blobs)
            {
                var newFileData = new FileData() { FileName = item.Name  };
                list.Add(newFileData);
            }

            return list;
        }
    
        public async Task<FileData> GetInfoFile(string fileName)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            // Create the container and return a container client object
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName); 

            var blobFile = containerClient.GetBlobClient(fileName);
            var fileInfoInMemory = await blobFile.DownloadAsync();

            MemoryStream ms = new MemoryStream();  

            await fileInfoInMemory.Value.Content.CopyToAsync(ms);
            
            var newFileData = new FileData() { FileName = blobFile.Name, FileInfo = ms.ToArray()  };

            return newFileData;
        }
    
    }

    public interface IAzureStorageService
    {
        Task SaveFileAsync(FileData file);
        Task<IEnumerable<FileData>> GetFiles();
        Task<FileData> GetInfoFile(string fileName);
    }
}