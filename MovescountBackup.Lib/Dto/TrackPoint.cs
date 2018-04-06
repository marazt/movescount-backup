using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MovescountBackup.Lib.Dto
{
    /// <summary>
    /// Movescount TrackPoint entity
    /// </summary>
    public class TrackPoint
    {
        [XmlElement("Altitude")]
        [JsonProperty(PropertyName = "Altitude")]
        public float Altitude { get; set; }

        [XmlElement("Bearing")]
        [JsonProperty(PropertyName = "Bearing")]
        public float Bearing { get; set; }

        [XmlElement("EHPE")]
        [JsonProperty(PropertyName = "EHPE")]
        public float Ehpe { get; set; }

        [XmlElement("Latitude")]
        [JsonProperty(PropertyName = "Latitude")]
        public double Latitude { get; set; }

        [XmlElement("LocalTime")]
        [JsonProperty(PropertyName = "LocalTime")]
        public string LocalTime { get; set; }

        [XmlElement("Longitude")]
        [JsonProperty(PropertyName = "Longitude")]
        public double Longitude { get; set; }

        [XmlElement("Speed")]
        [JsonProperty(PropertyName = "Speed")]
        public double Speed { get; set; }
    }
}
