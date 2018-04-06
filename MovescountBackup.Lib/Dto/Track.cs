using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MovescountBackup.Lib.Dto
{
    /// <summary>
    /// Movescount Track entity
    /// </summary>
    public class Track
    {
        [XmlElement("TrackPoints")]
        [JsonProperty(PropertyName = "TrackPoints")]
        public List<TrackPoint> TrackPoints { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Track"/> class.
        /// </summary>
        public Track()
        {
            this.TrackPoints = new List<TrackPoint>();
        }
    }
}
