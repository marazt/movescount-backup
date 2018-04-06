using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MovescountBackup.Lib.Dto
{
    /// <summary>
    /// Movescount MoveMark entity
    /// </summary>
    public class MoveMark
    {
        [XmlElement("DistanceFromPrevious")]
        [JsonProperty(PropertyName = "DistanceFromPrevious")]
        public int DistanceFromPrevious { get; set; }

        [XmlElement("SplitTimeFromPrevious")]
        [JsonProperty(PropertyName = "SplitTimeFromPrevious")]
        public double SplitTimeFromPrevious { get; set; }

        [XmlElement("Type")]
        [JsonProperty(PropertyName = "Type")]
        public int Type { get; set; }

        [XmlElement("Notes")]
        [JsonProperty(PropertyName = "Notes")]
        public string Notes { get; set; }
    }
}
