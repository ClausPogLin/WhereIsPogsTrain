using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsPogsTrain.Models;

namespace WhereIsPogsTrain
{
    public static class Data
    {
        public const string GET_TRAIN_LOCATION_URL         = "https://cdmetro.cnzhiyuanhui.com/op/trains/all-locations";
        public const string GET_RAILWAY_NETWORK_DETAIL_URL = "https://cdmetro.cnzhiyuanhui.com/op/station-time";
        public const string GET_STATION_DETAIL_URL         = "https://cdmetro.cnzhiyuanhui.com/op/stations/";

        public const string GET_STATION_NEARBY_TRAIN_DISPLAY_URL
            = "https://cdmetro.cnzhiyuanhui.com/op/trains/nearby-station";

        public const string GET_STATION_DISTANCE_URL = "https://cdmetro.cnzhiyuanhui.com/op/station-distance";
        public const long   INFINTE_NUM              = 999999;
        public const string DIVIDED_LINE_TEXT        = "------------------------------";
        public const string RETURN                   = "\n";
    }

    public struct DijkstraCalculateVar
    {
        public long[]       Distance;
        public long[]       PrevStation;
        public List<long>[] PrevPath;
        public long[]       Processed;
    }

    public struct LineName
    {
        public string lineNameEn;
        public string lineNameZh;
        public string lineColorHex;
        public string lineNo;
    }
}