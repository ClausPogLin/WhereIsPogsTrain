﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using WhereIsPogsTrain.Models;
//
//    var getStationNearbyTrainDisplay = GetStationNearbyTrainDisplay.FromJson(jsonString);

namespace WhereIsPogsTrain.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GetStationNearbyTrainDisplay
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public GetStationNearbyTrainDisplay_Data Data { get; set; }
    }

    public partial class GetStationNearbyTrainDisplay_Data
    {
        [JsonProperty("list")]
        public List<GetStationNearbyTrainDisplay_List> List { get; set; }
    }

    public partial class GetStationNearbyTrainDisplay_List
    {
        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("endStationName")]
        public string EndStationName { get; set; }

        [JsonProperty("trainList")]
        public List<TrainList> TrainList { get; set; }
    }

    public partial class TrainList
    {
        [JsonProperty("trainId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrainId { get; set; }

        [JsonProperty("trainStationNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrainStationNo { get; set; }

        [JsonProperty("trainStationName")]
        public string TrainStationName { get; set; }

        [JsonProperty("trainStationNameEn")]
        public string TrainStationNameEn { get; set; }

        [JsonProperty("trainNextStationNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrainNextStationNo { get; set; }

        [JsonProperty("trainNextStationName")]
        public string TrainNextStationName { get; set; }

        [JsonProperty("trainNextStationNameEn")]
        public string TrainNextStationNameEn { get; set; }

        [JsonProperty("nextStationNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long NextStationNo { get; set; }

        [JsonProperty("nextStationName")]
        public string NextStationName { get; set; }

        [JsonProperty("nextStationNameEn")]
        public string NextStationNameEn { get; set; }

        [JsonProperty("endStationNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long EndStationNo { get; set; }

        [JsonProperty("endStationName")]
        public string EndStationName { get; set; }

        [JsonProperty("endStationNameEn")]
        public string EndStationNameEn { get; set; }

        [JsonProperty("groupId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long GroupId { get; set; }

        [JsonProperty("themeName")]
        public string ThemeName { get; set; }

        [JsonProperty("trainType")]
        public long TrainType { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }
    }

    public partial class GetStationNearbyTrainDisplay
    {
        public static GetStationNearbyTrainDisplay FromJson(string json) => JsonConvert.DeserializeObject<GetStationNearbyTrainDisplay>(json, WhereIsPogsTrain.Models.Converter.Settings);
    }    
}