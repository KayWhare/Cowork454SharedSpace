using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Azure;

namespace CoWork454.Common
{
    public static class AzureStorage
    {
        public static string AddUpdateFile(string fileName, Stream fileStream, string connectionString, string containerName = null)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            var blobServiceClient = new BlobServiceClient(connectionString);
            // Create a unique name for the container if none provided
            if (string.IsNullOrEmpty(containerName))
            {
                containerName = Guid.NewGuid().ToString("N");
            }
            // Create the container and return a container client object
            containerName = containerName.ToLower();
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists(PublicAccessType.Blob);
            // create the blob and upload the data to it
            var blobClient = containerClient.GetBlobClient(fileName);
            blobClient.Upload(fileStream, true);
            blobClient.SetHttpHeaders(new BlobHttpHeaders
            {
                ContentDisposition = $"inline; filename={fileName}"
            });
            return blobClient.Uri.ToString();
        }
    }
}