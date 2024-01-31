﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using WhereIsPogsTrain.Models;
//
//    var getStationDetail = GetStationDetail.FromJson(jsonString);

namespace WhereIsPogsTrain.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Text.Json.Serialization;
    using System.Text.Json;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using JsonConverter = Newtonsoft.Json.JsonConverter;
    using JsonConverterAttribute = Newtonsoft.Json.JsonConverterAttribute;
    using JsonSerializer = Newtonsoft.Json.JsonSerializer;

    public partial class GetStationDetail
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
        [JsonProperty("stationDirecInfo")]
        public List<StationDirecInfo> StationDirecInfo { get; set; }

        [JsonProperty("stationNo")]
        public string StationNo { get; set; }

        [JsonProperty("stationName")]
        public string StationName { get; set; }

        [JsonProperty("stationCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long StationCode { get; set; }

        [JsonProperty("stationCodes")]
        public List<StationCode> StationCodes { get; set; }

        [JsonProperty("isUrgentLimit")]
        public bool IsUrgentLimit { get; set; }

        [JsonProperty("transferLines")]
        public List<string> TransferLines { get; set; }

        [JsonProperty("facilities")]
        public Facilities Facilities { get; set; }

        [JsonProperty("entrances")]
        public Entrances Entrances { get; set; }

        [JsonProperty("currentLimiting")]
        public List<object> CurrentLimiting { get; set; }
    }

    public partial class Entrances
    {
        [JsonProperty("roundInfo")]
        public List<RoundInfo> RoundInfo { get; set; }
    }

    public partial class RoundInfo
    {
        [JsonProperty("entry")]
        public string Entry { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("busStations")]
        public List<BusStation> BusStations { get; set; }
    }

    public partial class BusStation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("list")]
        public string List { get; set; }

        [JsonProperty("distance")]
        public object Distance { get; set; }
    }

    public partial class Facilities
    {
        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("cover")]
        public string Cover { get; set; }

        [JsonProperty("stationFacilities")]
        public List<StationFacility> StationFacilities { get; set; }

        [JsonProperty("shops")]
        public string Shops { get; set; }
    }

    public partial class StationFacility
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public partial class StationCode
    {
        [JsonProperty("lineNo")]
        public string LineNo { get; set; }

        [JsonProperty("stationCode")]
        public string StationCodeStationCode { get; set; }
    }

    public partial class StationDirecInfo
    {
        [JsonProperty("lineNo")]
        public string LineNo { get; set; }

        [JsonProperty("lineName")]
        public string LineName { get; set; }

        [JsonProperty("stationNo")]
        public string StationNo { get; set; }

        [JsonProperty("stationName")]
        public string StationName { get; set; }

        [JsonProperty("endStation")]
        public List<EndStation> EndStation { get; set; }
    }

    public partial class EndStation
    {
        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("stationNo")]
        public string StationNo { get; set; }

        [JsonProperty("stationName")]
        public string StationName { get; set; }

        [JsonProperty("nextStationNo")]
        public string NextStationNo { get; set; }

        [JsonProperty("nextStationName")]
        public string NextStationName { get; set; }

        [JsonProperty("direc")]
        public string Direc { get; set; }
    }

    public partial class GetStationDetail
    {
        public static GetStationDetail FromJson(string json) => JsonConvert.DeserializeObject<GetStationDetail>(json, WhereIsPogsTrain.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GetStationDetail self) => JsonConvert.SerializeObject(self, WhereIsPogsTrain.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}