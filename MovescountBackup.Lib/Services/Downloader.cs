using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MovescountBackup.Lib.Dto;
using MovescountBackup.Lib.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MovescountBackup.Lib.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// TODO Edit XML Comment Template for Downloader
    public class Downloader
    {
        // ReSharper disable once MemberCanBePrivate.Global
        /// <summary>
        /// The move data file
        /// </summary>
        public const string MoveDataFile = "move_data.json";

        /// <summary>
        /// Gets the authentication query.
        /// </summary>
        /// <value>
        /// The authentication query.
        /// </value>
        private string AuthQuery { get; }

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The client
        /// </summary>
        private readonly IClient client;

        /// <summary>
        /// The storage
        /// </summary>
        private readonly IStorage storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="Downloader"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="client">The client.</param>
        /// <param name="storage">The storage.</param>
        /// <param name="logger">The logger.</param>
        public Downloader(IConfiguration configuration, IClient client, IStorage storage, ILogger logger)
        {
            this.configuration = configuration;
            this.client = client;
            this.storage = storage;
            this.logger = logger;
            this.AuthQuery =
    $"appkey={configuration.MovescountAppKey}&email={configuration.MovescountUserEmail}&userkey={configuration.MovescountUserKey}";
        }

        /// <summary>
        /// Downloads the last user moves.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>List of moves</returns>
        public async Task<IList<Move>> DownloadLastUserMoves(string username)
        {
            const int dateRange = 10;
            var to = DateTime.UtcNow;
            var from = to.AddDays(-dateRange);
            var moves = await this.client.GetUserMoves(from, to, username);
            var allMoves = new List<Move>();

            while (moves.Any())
            {
                foreach (var move in moves)
                {
                    var downloadedMove = await this.DownloadMove(move.MoveId);
                    if (downloadedMove == null)
                    {
                        return allMoves;
                    }
                    allMoves.Add(move);
                }

                to = from;
                var days = moves.Count == 10 ? dateRange / 2 : dateRange;
                from = to.AddDays(-days);
                moves = await this.client.GetUserMoves(from, to, username);
            }

            return allMoves;
        }

        /// <summary>
        /// Gets the and store data.
        /// </summary>
        /// <param name="urlPart">The URL part.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Data string</returns>
        private async Task<string> GetAndStoreData(string urlPart, string fileName)
        {
            this.logger.LogInformation($"Downloading of {urlPart} started");
            var url = new Uri(new Uri(Client.BaseUrl), urlPart);
            string data;
            using (var httpClient = new HttpClient())
            {
                data = await httpClient.GetStringAsync(url);
            }

            await this.storage.StoreData(fileName, data);
            this.logger.LogInformation($"Downloading of {urlPart} done");
            return data;
        }

        /// <summary>
        /// Gets the and store GPS data.
        /// </summary>
        /// <param name="moveId">The move identifier.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <param name="exportFormat">The export format.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Task result</returns>
        private async Task GetAndStoreGpsData(long moveId, string cookieValue, ExportFormatEnum exportFormat,
            string fileName)
        {
            if (string.IsNullOrWhiteSpace(configuration.CookieValue))
            {
                this.logger.LogWarning("Cookie values is not set - could not download GPS data");
                return;
            }
            
            this.logger.LogInformation($"Downloading of GPS data in {exportFormat.ToString()} format started");
            var data = await this.client.GetGspDataFile(moveId, exportFormat, cookieValue);

            await this.storage.StoreData(fileName, data);
            this.logger.LogInformation($"Downloading of GPS data in {exportFormat.ToString()} format done");
        }

        /// <summary>
        /// Creates the name of the file.
        /// </summary>
        /// <param name="rootDir">The root dir.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>File name</returns>
        private static string CreateFileName(string rootDir, string fileName) => Path.Combine(rootDir, fileName);

        /// <summary>
        /// Creates the name of the GPS file map.
        /// </summary>
        /// <param name="exportFormat">The export format.</param>
        /// <returns>GPS file name</returns>
        private static string CreateGpsFileMapName(ExportFormatEnum exportFormat) =>
            $"gps_data.{exportFormat.ToString().ToLower()}";

        /// <summary>
        /// Downloads the move.
        /// </summary>
        /// <param name="moveId">The move identifier.</param>
        /// <returns>Move instance</returns>
        private async Task<Move> DownloadMove(long moveId)
        {
            this.logger.LogInformation($"Downloading of move {moveId} started.");
            var moveDir = string.IsNullOrWhiteSpace(this.configuration.BackupDir) ? moveId.ToString() : Path.Combine(this.configuration.BackupDir, moveId.ToString());
            var moveDataFile = CreateFileName(moveDir, MoveDataFile);

            if (await this.storage.FileExists(moveDataFile))
            {
                this.logger.LogInformation($"Move {moveId} is already downloaded.");
                return null;
            }
            
            var mediaDir = Path.Combine(moveDir, "media");

            this.storage.CreateDirectory(moveDir);
            this.storage.CreateDirectory(mediaDir);

            this.logger.LogInformation("Downloading of move data started.");
            var move = await this.client.GetMove(moveId);
            await this.storage.StoreData(moveDataFile, JsonConvert.SerializeObject(move));
            this.logger.LogInformation("Downloading of move data done.");

            await this.GetAndStoreData($"{move.TrackURI}?{this.AuthQuery}",
                CreateFileName(moveDir, "track_data.json"));
            await this.GetAndStoreData($"{move.MarksURI}?{this.AuthQuery}",
                CreateFileName(moveDir, "marks_data.json"));
            await this.GetAndStoreData($"{move.SamplesURI}?{this.AuthQuery}",
                CreateFileName(moveDir, "samples_data.json"));

            await this.GetAndStoreGpsData(moveId, this.configuration.CookieValue, ExportFormatEnum.Kml,
                CreateFileName(moveDir, CreateGpsFileMapName(ExportFormatEnum.Kml)));
            await this.GetAndStoreGpsData(moveId, this.configuration.CookieValue, ExportFormatEnum.Gpx,
                CreateFileName(moveDir, CreateGpsFileMapName(ExportFormatEnum.Gpx)));
            await this.GetAndStoreGpsData(moveId, this.configuration.CookieValue, ExportFormatEnum.Xlsx,
                CreateFileName(moveDir, CreateGpsFileMapName(ExportFormatEnum.Xlsx)));
            await this.GetAndStoreGpsData(moveId, this.configuration.CookieValue, ExportFormatEnum.Fit,
                CreateFileName(moveDir, CreateGpsFileMapName(ExportFormatEnum.Fit)));
            await this.GetAndStoreGpsData(moveId, this.configuration.CookieValue, ExportFormatEnum.Tcx,
                CreateFileName(moveDir, CreateGpsFileMapName(ExportFormatEnum.Tcx)));

            var mediaResponse = await this.GetAndStoreData($"{move.MediaResourcesURI}?{this.AuthQuery}",
                CreateFileName(moveDir, "media_data.json"));
            if (mediaResponse != null && !string.IsNullOrWhiteSpace(mediaResponse.Trim()))
            {
                var mediaData = JArray.Parse(mediaResponse);

                foreach (var media in mediaData)
                {
                    if (!(media is JObject mediaNode))
                    {
                        continue;
                    }
                    
                    var url = mediaNode.GetValue("URIOriginal").ToString();
                    var urlParts = url.Split('/');
                    using (var httpClient = new HttpClient())
                    {
                        var data = await httpClient.GetByteArrayAsync(url);
                        await this.storage.StoreData(CreateFileName(mediaDir, urlParts[urlParts.Length - 1]), data);
                    }
                }
            }

            this.logger.LogInformation($"Downloading of move {moveId} done.");
            return move;
        }
    }
}
