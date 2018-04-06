namespace MovescountBackup.Lib
{

    /// <summary>
    /// Configuration interface
    /// </summary>
    public interface IConfiguration
    {
        string MovescountAppKey { get; }

        string MovescountUserEmail { get; }

        string MovescountUserKey { get; }
        
        string MovescountMemberName { get; }

        string BackupDir { get; }

        string CookieValue { get; }
        
        string StorageConnectionString { get; }

        string ContainerName { get; }
    }
}
