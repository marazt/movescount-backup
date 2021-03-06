﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace MovescountBackup.Lib.Services
{
    /// <summary>
    /// Azure Cloud Storage 
    /// </summary>
    /// <seealso cref="MovescountBackup.Lib.Services.IStorage" />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class CloudStorage: IStorage
    {
        /// <summary>
        /// The Blob client
        /// </summary>
        private readonly CloudBlobClient blobClient;

        /// <summary>
        /// The main container name
        /// </summary>
        private readonly string mainContainerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CloudStorage"/> class.
        /// </summary>
        /// <param name="storageConnectionString">The storage connection string.</param>
        /// <param name="mainContainerName">Name of the main container.</param>
        public CloudStorage(string storageConnectionString, string mainContainerName)
        {
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            this.blobClient = storageAccount.CreateCloudBlobClient();
            this.mainContainerName = mainContainerName;
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <returns></returns>
        private async Task<CloudBlobContainer> GetContainer(string containerName)
        {
            var container = this.blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            return container;
        }

        /// <summary>
        /// Gets the Blob of container.
        /// </summary>
        /// <param name="containerName">Name of the container.</param>
        /// <param name="blobName">Name of the BLOB.</param>
        /// <returns></returns>
        private async Task<CloudBlockBlob> GetBlobOfContainer(string containerName, string blobName)
        {
            var container = await this.GetContainer(containerName);
            return container.GetBlockBlobReference(blobName);
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Data string
        /// </returns>
        public async Task<string> LoadData(string fileName)
        {
            var blob = await this.GetBlobOfContainer(this.mainContainerName, fileName);
            var data = await blob.DownloadTextAsync();
            return data;
        }

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Task with operation result
        /// </returns>
        public async Task<bool> FileExists(string fileName)
        {
            var blob = await this.GetBlobOfContainer(this.mainContainerName, fileName);
            var exists = await blob.ExistsAsync();
            return exists;
        }

        /// <summary>
        /// Stores the data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// Task result
        /// </returns>
        public async Task StoreData(string fileName, string data)
        {
            var blob = await this.GetBlobOfContainer(this.mainContainerName, fileName);
            await blob.UploadTextAsync(data);
        }

        /// <summary>
        /// Stores the data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="data">The data.</param>
        /// <returns>
        /// Task result
        /// </returns>
        public async Task StoreData(string fileName, byte[] data)
        {
            var blob = await this.GetBlobOfContainer(this.mainContainerName, fileName);
            await blob.UploadFromByteArrayAsync(data, 0, data.Length);
        }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public void CreateDirectory(string directoryName)
        {

        }
    }
}
