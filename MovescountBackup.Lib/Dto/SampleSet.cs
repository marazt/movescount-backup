using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MovescountBackup.Lib.Dto
{
    /// <summary>
    /// Movescount SampleSet entity
    /// </summary>
    public class SampleSet
    {
        [XmlElement("Altitude")]
        [JsonProperty(PropertyName = "Altitude")]
        public float Altitude { get; set; }

        [XmlElement("BikeCadence")]
        [JsonProperty(PropertyName = "BikeCadence")]
        public float BikeCadence { get; set; }

        [XmlElement("Distance")]
        [JsonProperty(PropertyName = "Distance")]
        public int Distance { get; set; }

        [XmlElement("Energy")]
        [JsonProperty(PropertyName = "Energy")]
        public float Energy { get; set; }

        [XmlElement("HeartRate")]
        [JsonProperty(PropertyName = "HeartRate")]
        public int HeartRate { get; set; }

        [XmlElement("LocalTime")]
        [JsonProperty(PropertyName = "LocalTime")]
        public string LocalTime { get; set; }

        [XmlElement("Power")]
        [JsonProperty(PropertyName = "Power")]
        public int Power { get; set; }

        [XmlElement("RunningCadence")]
        [JsonProperty(PropertyName = "RunningCadence")]
        public float RunningCadence { get; set; }

        [XmlElement("Speed")]
        [JsonProperty(PropertyName = "Speed")]
        public float Speed { get; set; }

        [XmlElement("Temperature")]
        [JsonProperty(PropertyName = "Temperature")]
        public float Temperature { get; set; }

        [XmlElement("VerticalSpeed")]
        [JsonProperty(PropertyName = "VerticalSpeed")]
        public float VerticalSpeed { get; set; }
    }
}
