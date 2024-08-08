using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhereIsPogsTrain.Models;

namespace WhereIsPogsTrain
{
    public class APIs
    {
        public GetLineTrainLocation? GetLineTrainLocation(string lineNo)
        {
            var client  = new RestClient(Data.GET_TRAIN_LOCATION_URL);
            var request = new RestRequest { Method = Method.Get };
            var body    = @"{""lineNo"":" + lineNo + "}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Get(request);
            return Models.GetLineTrainLocation.FromJson(response.Content) ?? null;
        }

        public GetStationDistance? GetStationDistance(string lineNo)
        {
            var client  = new RestClient(Data.GET_STATION_DISTANCE_URL);
            var request = new RestRequest { Method = Method.Get };

            //var body = @"{""lineNo"":" + lineNo + @"}";
            request.AddQueryParameter("lineNo", lineNo);

            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Get(request);
            return Models.GetStationDistance.FromJson(response.Content) ?? null;
        }
        
        public GetStationDistance? GetStationDisAtanceA(string lineNo)
        {
            var client  = new RestClient(Data.GET_STATION_DISTANCE_URL);
            var request = new RestRequest { Method = Method.Get };

            //var body = @"{""lineNo"":" + lineNo + @"}";
            request.AddQueryParameter("lineNo", lineNo);

            //request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response;
            try
            {
                response = client.Get(request);
            }
            catch(Exception e)
            {
                return null;
            }
            return Models.GetStationDistance.FromJson(response.Content) ?? null;
        }

        public GetRailwayNetworkDetail? GetRailwayNetworkDetail()
        {
            var          client   = new RestClient(Data.GET_RAILWAY_NETWORK_DETAIL_URL);
            var          request  = new RestRequest { Method = Method.Get };
            RestResponse response = client.Get(request);
            return Models.GetRailwayNetworkDetail.FromJson(response.Content) ?? null;
        }

        public GetStationDetail? GetStationDetail(string stationNo)
        {
            var          client   = new RestClient(Data.GET_STATION_DETAIL_URL + "/" + stationNo);
            var          request  = new RestRequest { Method = Method.Get };
            RestResponse response = client.Get(request);
            return Models.GetStationDetail.FromJson(response.Content) ?? null;
        }

        public GetStationNearbyTrainDisplay? GetStationNearbyTrainDisplay(string lineNo, string stationNo)
        {
            var client  = new RestClient(Data.GET_STATION_NEARBY_TRAIN_DISPLAY_URL);
            var request = new RestRequest { Method = Method.Get };
            var body    = @"{""lineNo"":" + lineNo + "," + "\n" + @"""stationNo"":" + stationNo + "}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Get(request);
            return Models.GetStationNearbyTrainDisplay.FromJson(response.Content) ?? null;
        }
    }
}