using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MovescountBackup.Lib;
using MovescountBackup.Lib.Services;

namespace MovescountBackup.Console
{
    /// <summary>
    /// Sample app
    /// </summary>
    static class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static async Task Main(string[] args)
        {
            var configuration = SetupConfiguration();
            var client = new Client(configuration, new ConsoleLogger<Client>());
            var storage = new CloudStorage(configuration.StorageConnectionString, configuration.ContainerName);
            var downloader = new Downloader(configuration, client, storage, new ConsoleLogger<Downloader>());
            var moves = await downloader.DownloadLastUserMoves(configuration.MovescountMemberName);
          
            System.Console.WriteLine($"{moves.Count} moves have been downloaded");
        }


        private static Lib.IConfiguration SetupConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            return new Configuration(configuration);
        }
    }
}