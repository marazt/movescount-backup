using Microsoft.Extensions.Configuration;

namespace MovescountBackup.Lib
{

    /// <summary>
    /// Configuration
    /// </summary>
    /// <seealso cref="MovescountBackup.Lib.IConfiguration" />
    public class Configuration : IConfiguration
    {
        private readonly IConfigurationRoot root;

        public Configuration(IConfigurationRoot root)
        {
            this.root = root;
        }

        public string MovescountAppKey => this.root["AppConfig:MovescountAppKey"];

        public string MovescountUserEmail => this.root["AppConfig:MovescountUserEmail"];

        public string MovescountUserKey => this.root["AppConfig:MovescountUserKey"];
        
        public string MovescountMemberName => this.root["AppConfig:MovescountMemberName"];

        public string BackupDir => this.root["AppConfig:BackupDir"];

        public string CookieValue => this.root["AppConfig:CookieValue"];
        
        public string StorageConnectionString => this.root["AppConfig:StorageConnectionString"];
        
        public string ContainerName => this.root["AppConfig:ContainerName"];
    }
}
