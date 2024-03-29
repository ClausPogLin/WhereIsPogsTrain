﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using WhereIsPogsTrain.Models;
//
//    var getLineTrainLocation = GetLineTrainLocation.FromJson(jsonString);

namespace WhereIsPogsTrain.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class GetLineTrainLocation
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("list")]
        public List<List> List { get; set; }
    }

    public partial class List
    {
        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("trainsLos")]
        public List<TrainsLo> TrainsLos { get; set; }
    }

    public partial class TrainsLo
    {
        [JsonProperty("trainId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrainId { get; set; }

        [JsonProperty("compAxle")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CompAxle { get; set; }

        [JsonProperty("trainStationNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrainStationNo { get; set; }

        [JsonProperty("trainStationName")]
        public string TrainStationName { get; set; }

        [JsonProperty("trainNextStationNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrainNextStationNo { get; set; }

        [JsonProperty("trainNextStationName")]
        public string TrainNextStationName { get; set; }

        [JsonProperty("axleCount")]
        public long AxleCount { get; set; }

        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("trainNo")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TrainNo { get; set; }

        [JsonProperty("expressType")]
        public long ExpressType { get; set; }

        [JsonProperty("groupId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long GroupId { get; set; }

        [JsonProperty("themeName")]
        public ThemeName ThemeName { get; set; }

        [JsonProperty("trainType")]
        public long TrainType { get; set; }
    }

    public enum ThemeName { Local, Express };

    public partial class GetLineTrainLocation
    {
        public static GetLineTrainLocation FromJson(string json) => JsonConvert.DeserializeObject<GetLineTrainLocation>(json, WhereIsPogsTrain.Models.Converter.Settings);
    }

    internal class ThemeNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ThemeName) || t == typeof(ThemeName?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "普通列车":
                    return ThemeName.Local;
                case "直达列车":
                    return ThemeName.Express;
            }
            throw new Exception("Cannot unmarshal type ThemeName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ThemeName)untypedValue;
            switch (value)
            {
                case ThemeName.Local:
                    serializer.Serialize(writer, "普通列车");
                    return;
                case ThemeName.Express:
                    serializer.Serialize(writer, "直达列车");
                    return;
            }
            throw new Exception("Cannot marshal type ThemeName");
        }

        public static readonly ThemeNameConverter Singleton = new ThemeNameConverter();
    }
}
