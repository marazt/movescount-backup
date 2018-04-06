using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using MovescountBackup.Lib.Enums;
using Newtonsoft.Json;

namespace MovescountBackup.Lib.Dto
{
    /// <summary>
    /// Movescount Move entity
    /// </summary>
    [XmlRoot("Move")]
    public class Move
    {
        [XmlElement("ActivityID")]
        [JsonProperty(PropertyName = "ActivityID")]
        public ActivityIdEnum ActivityID { get; set; }

        [XmlElement("AscentAltitude")]
        [JsonProperty(PropertyName = "AscentAltitude")]
        public double? AscentAltitude { get; set; }

        [XmlElement("AscentTime")]
        [JsonProperty(PropertyName = "AscentTime")]
        public double? AscentTime { get; set; }

        [XmlElement("AvgCadence")]
        [JsonProperty(PropertyName = "AvgCadence")]
        public float? AvgCadence { get; set; }

        [XmlElement("AvgBikeCadence")]
        [JsonProperty(PropertyName = "AvgBikeCadence")]
        public float? AvgBikeCadence { get; set; }

        [XmlElement("AvgRunningCadence")]
        [JsonProperty(PropertyName = "AvgRunningCadence")]
        public float? AvgRunningCadence { get; set; }

        [XmlElement("AvgHR")]
        [JsonProperty(PropertyName = "AvgHR")]
        public int? AvgHR { get; set; }

        [XmlElement("AvgSpeed")]
        [JsonProperty(PropertyName = "AvgSpeed")]
        public float? AvgSpeed { get; set; }

        [XmlElement("AvgTemp")]
        [JsonProperty(PropertyName = "AvgTemp")]
        public float? AvgTemp { get; set; }

        [XmlElement("AvgPower")]
        [JsonProperty(PropertyName = "AvgPower")]
        public float? AvgPower { get; set; }

        [XmlElement("DescentAltitude")]
        [JsonProperty(PropertyName = "DescentAltitude")]
        public double? DescentAltitude { get; set; }

        [XmlElement("DescentTime")]
        [JsonProperty(PropertyName = "DescentTime")]
        public double? DescentTime { get; set; }

        [XmlElement("Distance")]
        [JsonProperty(PropertyName = "Distance")]
        public int? Distance { get; set; }

        [XmlElement("Duration")]
        [JsonProperty(PropertyName = "Duration")]
        public double Duration { get; set; }

        [XmlElement("Energy")]
        [JsonProperty(PropertyName = "Energy")]
        public int? Energy { get; set; }

        [XmlElement("Feeling")]
        [JsonProperty(PropertyName = "Feeling")]
        public FeelingEnum? Feeling { get; set; }

        [XmlElement("FlatTime")]
        [JsonProperty(PropertyName = "FlatTime")]
        public double? FlatTime { get; set; }

        [XmlElement("HighAltitude")]
        [JsonProperty(PropertyName = "HighAltitude")]
        public float? HighAltitude { get; set; }

        [XmlElement("LastModifiedDate")]
        [JsonProperty(PropertyName = "LastModifiedDate")]
        public string LastModifiedDate { get; set; }

        [XmlElement("LocalStartTime")]
        [JsonProperty(PropertyName = "LocalStartTime")]
        public DateTime? LocalStartTime { get; set; }

        [XmlElement("UTCStartTime")]
        [JsonProperty(PropertyName = "UTCStartTime")]
        public DateTime? UTCStartTime { get; set; }

        [XmlElement("LowAltitude")]
        [JsonProperty(PropertyName = "LowAltitude")]
        public float? LowAltitude { get; set; }

        [XmlElement("MaxBikeCadence")]
        [JsonProperty(PropertyName = "MaxBikeCadence")]
        public float? MaxBikeCadence { get; set; }

        [XmlElement("MaxRunningCadence")]
        [JsonProperty(PropertyName = "MaxRunningCadence")]
        public float? MaxRunningCadence { get; set; }

        [XmlElement("MaxSpeed")]
        [JsonProperty(PropertyName = "MaxSpeed")]
        public float? MaxSpeed { get; set; }

        [XmlElement("MaxTemp")]
        [JsonProperty(PropertyName = "MaxTemp")]
        public float? MaxTemp { get; set; }

        [XmlElement("MinTemp")]
        [JsonProperty(PropertyName = "MinTemp")]
        public float? MinTemp { get; set; }

        [XmlElement("MaxPower")]
        [JsonProperty(PropertyName = "MaxPower")]
        public float? MaxPower { get; set; }

        [XmlElement("MemberID")]
        [JsonProperty(PropertyName = "MemberID")]
        public int MemberId { get; set; }

        [XmlElement("MinHR")]
        [JsonProperty(PropertyName = "MinHR")]
        public int? MinHR { get; set; }

        [XmlElement("MoveID")]
        [JsonProperty(PropertyName = "MoveID")]
        public int MoveId { get; set; }

        [XmlElement("Notes")]
        [JsonProperty(PropertyName = "Notes")]
        public string Notes { get; set; }

        [XmlElement("PeakHR")]
        [JsonProperty(PropertyName = "PeakHR")]
        public int? PeakHR { get; set; }

        [XmlElement("SessionName")]
        [JsonProperty(PropertyName = "SessionName")]
        public string SessionName { get; set; }

        [XmlElement("StartLatitude")]
        [JsonProperty(PropertyName = "StartLatitude")]
        public double? StartLatitude { get; set; }

        [XmlElement("StartLongitute")]
        [JsonProperty(PropertyName = "StartLongitute")]
        public double? StartLongitute { get; set; }

        [XmlElement("Tags")]
        [JsonProperty(PropertyName = "Tags")]
        public string Tags { get; set; }

        [XmlElement("TrainingEffect")]
        [JsonProperty(PropertyName = "TrainingEffect")]
        public float? TrainingEffect { get; set; }

        [XmlElement("Weather")]
        [JsonProperty(PropertyName = "Weather")]
        public WeatherEnum? Weather { get; set; }

        [XmlElement("DeviceName")]
        [JsonProperty(PropertyName = "DeviceName")]
        public string DeviceName { get; set; }

        [XmlElement("DeviceSerialNumber")]
        [JsonProperty(PropertyName = "DeviceSerialNumber")]
        public string DeviceSerialNumber { get; set; }


        [XmlElement("RecoveryTime")]
        [JsonProperty(PropertyName = "RecoveryTime")]
        public double? RecoveryTime { get; set; }

        [XmlElement("Samples")]
        [JsonProperty(PropertyName = "Samples")]
        public Samples Samples { get; set; }

        [XmlElement("SamplesURI")]
        [JsonProperty(PropertyName = "SamplesURI")]
        public string SamplesURI { get; set; }

        [XmlElement("Track")]
        [JsonProperty(PropertyName = "Track")]
        public Track Track { get; set; }

        [XmlElement("TrackURI")]
        [JsonProperty(PropertyName = "TrackURI")]
        public string TrackURI { get; set; }

        [XmlElement("Marks")]
        [JsonProperty(PropertyName = "Marks")]
        public List<MoveMark> Marks { get; set; }

        [XmlElement("MarksURI")]
        [JsonProperty(PropertyName = "MarksURI")]
        public string MarksURI { get; set; }
        
        [XmlElement("MediaResourcesURI")]
        [JsonProperty(PropertyName = "MediaResourcesURI")]
        public string MediaResourcesURI { get; set; }
    }
}
