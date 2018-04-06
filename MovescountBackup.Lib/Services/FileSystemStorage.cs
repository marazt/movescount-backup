using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace MovescountBackup.Lib.Services
{
    /// <summary>
    /// File system storage
    /// </summary>
    /// <seealso cref="MovescountBackup.Lib.Services.IStorage" />
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class FileSystemStorage : IStorage
    {
        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        public void CreateDirectory(string directoryName)
        {
            Directory.CreateDirectory(directoryName);
        }

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// Task with operation result
        /// </returns>
        public Task<bool> FileExists(string fileName)
        {
            return Task.FromResult(File.Exists(fileName));
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
            return await Task.FromResult(File.ReadAllText(fileName));
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
            File.WriteAllText(fileName, data);
            await Task.CompletedTask;
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
            File.WriteAllBytes(fileName, data);
            await Task.CompletedTask;
        }
    }
}
