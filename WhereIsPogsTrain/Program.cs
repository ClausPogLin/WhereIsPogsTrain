using WhereIsPogsTrain;
using WhereIsPogsTrain.Models;
using WhereIsPogsTrain.Algorithm;
using static WhereIsPogsTrain.Data;

namespace WhereIsPogsTrain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            APIs   apis                   = new APIs();
            string DestinationStationCode = "";
            string NowStationCode         = "";

            List<GetStationDistanceDatum> transEdges = new List<GetStationDistanceDatum>();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleHelper.Print("\n");
            ConsoleHelper.Print("Where is Pog's train?");
            ConsoleHelper.Print("\n");
            ConsoleHelper.Print("Getting Data...");
            var networkDetail = apis.GetRailwayNetworkDetail().Data;
            var lineList      = DataLanguageConverter.oriData2LineList(networkDetail);

            #region 构造图和依StationNo为节点编号的邻接矩阵

            Graph networkMap = new Graph(20005);
            ConsoleHelper.Print("Railway network data got.", ConsoleColor.Green, 1);
            foreach (var line in networkDetail)
            {
                ConsoleHelper.Print("Working on " + line.LineNameZh + "(" + line.LineNameEn + ").", ConsoleColor.Green,
                                    1);
                ConsoleHelper.Print("with " + line.SubLine.Count + " sublines.", ConsoleColor.Gray, 1);
                ConsoleHelper.Print("Waiting for network to cooldown...(500ms)", ConsoleColor.Gray, 2); //冷却
                Thread.Sleep(500);
                var lineDistanceDetail = apis.GetStationDistance(line.LineNo).Data; //获取站间距离api接口
                ConsoleHelper.Print("Distance data of " + line.LineNameZh + "(" + line.LineNameEn + ") got.",
                                    ConsoleColor.Green, 1);
                foreach (var distanceBetween in lineDistanceDetail)
                {
                    networkMap.insertEdge(Convert.ToInt32(distanceBetween.StartStation),
                                          Convert.ToInt32(distanceBetween.EndStation), distanceBetween.Distance);
                    networkMap.insertEdge(Convert.ToInt32(distanceBetween.EndStation),
                                          Convert.ToInt32(distanceBetween.StartStation), distanceBetween.Distance);
                }

                foreach (var subLine in line.SubLine)
                {
                    ConsoleHelper.Print(
                        "Working on subline " + subLine.Description + "(Direction " + subLine.Direction + ").", default,
                        2);
                    ConsoleHelper.Print("with " + subLine.StationList.Count + " stations.", ConsoleColor.Gray, 2);
                    if (subLine.Direction == "01") //01方向和02方向过站相同，方向相反，只向抽象图添加一次。
                    {
                        int _alreadyAddedStationNum = 0; //已存在站计数器
                        ConsoleHelper.Print("Adding " + subLine.StationList.Count + " stations to Graph.",
                                            ConsoleColor.Green, 3);
                        foreach (var station in subLine.StationList)
                        {
                            bool _alreadyAdded = false;
                            networkMap.insertEdge(Convert.ToInt32(station.StationNo),
                                                  Convert.ToInt32(station.StationNo), 0);
                            foreach (var stationItem in networkMap.vertexList)
                            {
                                if (((StationList)stationItem).StationNameZh ==
                                    station.StationNameZh) //(强制转换)+换乘站处理，遍历匹配名称
                                {
                                    networkMap.insertEdge(Convert.ToInt32(((StationList)stationItem).StationNo),
                                                          Convert.ToInt32(station.StationNo), 0);
                                    networkMap.insertEdge(Convert.ToInt32(station.StationNo),
                                                          Convert.ToInt32(((StationList)stationItem).StationNo), 0);

                                    //若图中已经添加过相同节点，仍然重复添加，但将二者邻接矩阵权重视为0(即为换乘站)
                                    _alreadyAdded = true;
                                    _alreadyAddedStationNum++; //重复标记和重复计数器+1
                                    Thread.Sleep(1);
                                    ConsoleHelper.Print(
                                        "Connecting " + ((StationList)stationItem).StationNameZh + "(Line No." +
                                        ((StationList)stationItem).StationCodes.First().LineNo + ") to " +
                                        station.StationNameZh + "(Line No." + station.StationCodes.First().LineNo +
                                        ").", ConsoleColor.Gray, 4);
                                }
                            }

                            if (_alreadyAdded == true)
                                ConsoleHelper.Print(
                                    "Because " + station.StationNameZh + "(" + station.StationNameEn + ")" +
                                    " is already in the graph, so in adjacency matrix, set weight to 0",
                                    ConsoleColor.DarkYellow, 4);
                            networkMap.insertVertex(station);
                        }

                        ConsoleHelper.Print(
                            "Done adding " + (subLine.StationList.Count - _alreadyAddedStationNum) +
                            " new stations (From " + subLine.StationList.First().StationNameZh + " to " +
                            subLine.StationList.Last().StationNameZh + ") to graph.", ConsoleColor.Green, 3);
                    }
                    else
                        ConsoleHelper.Print("Skipped,Direction is " + subLine.Direction + ".", ConsoleColor.DarkYellow,
                                            3);
                }
            }

            #endregion

            Search:
            ConsoleHelper.Print("Enter start StationCode：");
            NowStationCode = Console.ReadLine();
            ConsoleHelper.Print("Enter target StationCode：");
            DestinationStationCode = Console.ReadLine();
            var startStationDetailList = DataLanguageConverter.stationNo2StationList(NowStationCode, networkMap);
            var targetStationDetailList
                = DataLanguageConverter.stationNo2StationList(DestinationStationCode, networkMap);
            if (startStationDetailList == null || startStationDetailList == null)
            {
                ConsoleHelper.Print("Non-existent origin or destination station", ConsoleColor.Red, 1);
                goto Search;
            }

            ConsoleHelper.Print(DIVIDED_LINE_TEXT, ConsoleColor.Gray);
            ConsoleHelper.Print(
                "Station " + startStationDetailList.First().StationNameZh + "(" +
                startStationDetailList.First().StationNameEn + "),can transfer to Line " +
                startStationDetailList.First().TransferLines + " at this station", ConsoleColor.Yellow, 1);
            foreach (var transformLine in startStationDetailList.First().StationCodes)
            {
                ConsoleHelper.Print("(" + transformLine.LineNo + "|" + transformLine.StationCodeStationCode + ")",
                                    ConsoleColor.DarkYellow, 2);
            }

            DijkstraAlgorithmSeekPath.StartDijkstra(networkMap, lineList, startStationDetailList.First(),targetStationDetailList.First());
        }
    }
}