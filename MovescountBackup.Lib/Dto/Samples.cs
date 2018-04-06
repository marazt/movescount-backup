using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MovescountBackup.Lib.Dto
{
    /// <summary>
    /// Movescount Samples collection entity
    /// </summary>
    public class Samples
    {
        [XmlElement("SampleSets")]
        [JsonProperty(PropertyName = "SampleSets")]
        public List<SampleSet> SampleSets { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Samples"/> class.
        /// </summary>
        public Samples()
        {
            this.SampleSets = new List<SampleSet>();
        }
    }
}
