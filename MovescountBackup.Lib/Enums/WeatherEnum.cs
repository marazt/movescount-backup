using System.Diagnostics.CodeAnalysis;

namespace MovescountBackup.Lib.Enums
{
    /// <summary>
    /// Movescount weather enum
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum WeatherEnum
    {
        PartlyCloudy = 1,
        SunnySkies = 2,
        Cloudy = 3,
        LightRain = 4,
        HeavyRain = 5,
        Snowfall = 6,
        Dark = 7,
        Indoor = 8
    }
}
