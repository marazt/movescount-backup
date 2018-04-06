using System.Threading.Tasks;

namespace MovescountBackup.Lib.Services
{
    /// <summary>
    /// IStorage insterface
    /// </summary>
    public interface IStorage
    {
        // ReSharper disable once UnusedMember.Global
        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Data string</returns>
        Task<string> LoadData(string fileName);
      
        /// <summary>
        /// Stores the data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="data">The data.</param>
        /// <returns>Task result</returns>
        Task StoreData(string fileName, string data);

        /// <summary>
        /// Stores the data.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="data">The data.</param>
        /// <returns>Task result</returns>
        Task StoreData(string fileName, byte[] data);

        /// <summary>
        /// Files the exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Task with operation result</returns>
        Task<bool> FileExists(string fileName);
    
        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        void CreateDirectory(string directoryName);
    }
}
