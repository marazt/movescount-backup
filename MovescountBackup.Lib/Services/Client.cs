using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MovescountBackup.Lib.Dto;
using MovescountBackup.Lib.Enums;
using Newtonsoft.Json;

namespace MovescountBackup.Lib.Services
{
    /// <summary>
    /// Client implementation
    /// </summary>
    /// <seealso cref="MovescountBackup.Lib.Services.IClient" />
    public class Client : IClient
    {
        /// <summary>
        /// The base URL
        /// </summary>
        public const string BaseUrl = "https://uiservices.movescount.com";

        /// <summary>
        /// The export URL
        /// </summary>
        private const string ExportUrl = "http://www.movescount.com/move/export?id={0}&format={1}";
        
        /// <summary>
        /// The authentication query
        /// </summary>
        private readonly string authQuery;

        // ReSharper disable once NotAccessedField.Local
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        public Client(IConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.authQuery =
                $"appkey={configuration.MovescountAppKey}&email={configuration.MovescountUserEmail}&userkey={configuration.MovescountUserKey}";
        }

        /// <summary>
        /// Gets the move.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Move instance</returns>
        public async Task<Move> GetMove(long id)
        {
            var url = new Uri(new Uri(BaseUrl), $"moves/{id}?{this.authQuery}");
            this.logger.LogInformation($"Downloading move {id} from URL {url}");
            return await this.ExecuteUrlGetRequest<Move>(url.ToString());
        }

        /// <summary>
        /// Gets the user moves.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// List of moves
        /// </returns>
        public async Task<IList<Move>> GetUserMoves(DateTime from, DateTime to, string username)
        {
            var url = new Uri(new Uri(BaseUrl),
                $"members/{username}/moves?startdate={FormatDate(from)}&enddate={FormatDate(to)}&{this.authQuery}");
            this.logger.LogInformation(
                $"Downloading user moves from {from} to {to} of username {username} from URL {url}");
            return await this.ExecuteUrlGetRequest<IList<Move>>(url.ToString());
        }

        /// <summary>
        /// Gets the GSP data file.
        /// </summary>
        /// <param name="moveId">The move identifier.</param>
        /// <param name="exportFormat">The export format.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <returns>
        /// GPS data string
        /// </returns>
        public async Task<string> GetGpsDataFile(long moveId, ExportFormatEnum exportFormat, string cookieValue)
        {
            var url = string.Format(ExportUrl, moveId, exportFormat.ToString().ToLower());
            this.logger.LogInformation($"Downloading of GPS data of move {moveId} in {exportFormat.ToString()} format");
            
            return await this.ExecuteUrlGetRequest<string>(url, client =>
                {
                    client.DefaultRequestHeaders.Add("cookie", cookieValue);
                });
        }

        /// <summary>
        /// Executes the URL get request.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="setupClient">The setup client.</param>
        /// <returns>Type instance</returns>
        private async Task<T> ExecuteUrlGetRequest<T>(string url, Action<HttpClient> setupClient = null) where T : class
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    setupClient?.Invoke(httpClient);
                    var data = await httpClient.GetStringAsync(url);
                    return typeof(T) == typeof(string) ? data as T : JsonConvert.DeserializeObject<T>(data);
                }
            }
            catch (HttpRequestException hrex)
            {
                this.logger.LogError($"Http request error while executing requeset {url}");
                this.logger.LogError($"Http request error message {hrex.Message}");
                throw;
            }

            catch (Exception ex)
            {
                this.logger.LogError($"Error while executing requeset {url}");
                this.logger.LogError($"Error message {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Formatted date string</returns>
        private static string FormatDate(DateTime date) => date.ToString("yyyy-MM-dd");
    }
}