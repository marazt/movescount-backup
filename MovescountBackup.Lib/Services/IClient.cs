using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovescountBackup.Lib.Dto;
using MovescountBackup.Lib.Enums;

namespace MovescountBackup.Lib.Services
{
    /// <summary>
    /// Client interface
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Gets the move.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Move instance</returns>
        Task<Move> GetMove(long id);

        /// <summary>
        /// Gets the user moves.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="username">The username.</param>
        /// <returns>List of moves</returns>
        Task<IList<Move>> GetUserMoves(DateTime from, DateTime to, string username);

        /// <summary>
        /// Gets the GSP data file.
        /// </summary>
        /// <param name="moveId">The move identifier.</param>
        /// <param name="exportFormat">The export format.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <returns>GPS data string</returns>
        Task<string> GetGspDataFile(long moveId, ExportFormatEnum exportFormat, string cookieValue);
    }
}