# Movescount Backup

Movescount Backup is s simple library that can download move records from **Suunto Movescount**. I have created it just because there is no official way how to create a backup of all your moves and an optional possibility to migrate them to other service such as **Garmin** or **Strava**.

## Version

- **Version 1.0.1** - 2018-01-15

### Getting Started/Installing

```ps
Install-Package MovescountBackup -Version 1.0.1
```

## Project Description

The solution consists of the following projects:

- **MovescountBackup.Lib** is the main library containing client for downloading, main downloader, models and other services.

- **MovescountBackup.Console** is just a sample that can be run locally.

- **MovescountBackup.Lib.Spec** TODO.

### Prerequisites

- .NET Core 2.0.

### Configuration

- **MovescountAppKey** - App key to be able to query Movescount API.
- **MovescountUserKey** - User key to be able to query Movescount API.
- **MovescountUserEmail** - User email to be able to query Movescount API.
- **MovescountMemberName** - Name of the member whose data we want to get.
- **CookieValue** - A cookie value that is needed to export GPX, TCX and other move files.
    This value can be get by the following steps:
    1. Open console in your browser to se network requests.
    1. Login into Movescount.
    1. Select a requeset to `http://www.movescount.com/api/members/private/messages`.
    1. Copy value of `Cookie` key in request header. It should start with `ASP.NET ...`.
- **BackupDir** - Directory where moves should be stored. Required for `FileSystemStorage`.
- **StorageConnectionString** - Connection string to Azure Blob Storage. Required for `CloudStorate`.
- **ContainerName** - Container name on Azure Blob Storage. Required for `CloudStorate`.

## MovescountBackup.Lib

The main project

### Client.cs

Client is the class responsible for downloading data from Movescount. Because there is only need to get list of moves and one particular move with details,
there are no other calls, such as *create new move*, *update move*, etc. But it can be extended to a full Movescount API client.
The only problem is that the API specification is not public, so some inspection is required (e.g., searching on GitHub).

### Downloader.cs

A class that uses Client to download single or multiple moves and store them to local disk (see `FileSystemStorage.cs`) or Azure Blob Store (see `CloudStorage.cs`).
A sample Downloader usage is as follows (see `MovescountBackup.Console.Program.cs`):

```csharp
var configuration = SetupConfiguration(); // Load or set IConfiguration instance
var client = new Client(configuration, new ConsoleLogger<Client>());
var storage = new CloudStorage(configuration.StorageConnectionString, configuration.ContainerName);
var downloader = new Downloader(configuration, client, storage, new ConsoleLogger<Downloader>());
var moves = await downloader.DownloadLastUserMoves(configuration.MovescountMemberName);
Console.WriteLine($"{moves.Count} moves have been downloaded.");
```

## Deployment

You can use this libraly to be run manually (`MovescountBackup.Console.Program.cs`) or you can create, e.g., a *Azure function* or *Lambda function on AWS*, that can run periodically.

## Contributing

Any contribution is welcomed. Especially to `Client` class.

## Authors

- **Marek Polak** - *Initial work* - [marazt](https://github.com/marazt)

## License

© 2018 Marek Polak. This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgments

- Enjoy it!
- If you want, you can support this project too.