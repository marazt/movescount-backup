using System.Collections.Generic;
using System.Threading.Tasks;
using MovescountBackup.Lib.Dto;
using MovescountBackup.Lib.Enums;

namespace MovescountBackup.Lib.Services
{
    public interface IDownloader
    {
        /// <summary>
        /// Downloads the last user moves.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>List of moves</returns>
        Task<IList<Move>> DownloadLastUserMoves(string username);

        /// <summary>
        /// Gets the and store GPS data.
        /// </summary>
        /// <param name="moveId">The move identifier.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <param name="exportFormat">The export format.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Task result</returns>
        Task GetAndStoreGpsData(long moveId, string cookieValue, ExportFormatEnum exportFormat,
            string fileName);

        /// <summary>
        /// Downloads the move.
        /// </summary>
        /// <param name="moveId">The move identifier.</param>
        /// <returns>Move instance</returns>
        Task<Move> DownloadMove(long moveId);

        /// <summary>
        /// Creates the name of the GPS file map.
        /// </summary>
        /// <param name="exportFormat">The export format.</param>
        /// <returns>GPS file name</returns>
        string CreateGpsFileMapName(ExportFormatEnum exportFormat);
    }
}