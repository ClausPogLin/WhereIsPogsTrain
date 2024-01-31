using WhereIsPogsTrain.Models;
using static WhereIsPogsTrain.Data;

namespace WhereIsPogsTrain.Algorithm;

class GenerateGraphAdjacencyMatrix
{
    protected long[,] GraphMatrix;

    /// <summary>
    /// 导入已有的邻接矩阵
    /// </summary>
    /// <param name="graphMatrix"></param>
    protected GenerateGraphAdjacencyMatrix(long[,] graphMatrix)
    {
        GraphMatrix = graphMatrix;
        ConsoleHelper.Print("Adjacency matrix has been imported.", color: ConsoleColor.Green, layerIndex: 1);
    }

    /// <summary>
    /// 生成邻接矩阵
    /// </summary>
    /// <param name="weightPath"></param>
    /// <param name="number"></param>

    protected GenerateGraphAdjacencyMatrix(long[,] weightPath, long number)
    {
        GraphMatrix = new long[number + 5, number + 5];
        for (var i = 1; i < number; i++)
        {
            for (var j = 1; j < number; j++)
            {
                if (i != j)
                {
                    GraphMatrix[i, j] = INFINTE_NUM;
                }
                else
                {
                    GraphMatrix[i, j] = 0;
                }
            }
        }

        for (var i = 0; i < weightPath.GetLength(dimension: 0); i++)
        {
            var start                   = weightPath[i, 0];
            var end                     = weightPath[i, 1];
            GraphMatrix[start, end] = weightPath[i, 2];
        }
    }
}

class DijkstraAlgorithmSeekPath : GenerateGraphAdjacencyMatrix
{
    private DijkstraCalculateVar _calculateVar;

    /// <summary>
    /// 生成邻接矩阵并使用迪杰斯特拉算法寻最短路
    /// </summary>
    /// <param name="weightPath"></param>
    /// <param name="number"></param>
    public DijkstraAlgorithmSeekPath(long[,] weightPath, long number) : base(weightPath, number)
    {
        _calculateVar.Distance    = new long[number + 5];
        _calculateVar.PrevPath    = new List<long>[number + 5];
        _calculateVar.Processed   = new long[number + 5];
        _calculateVar.PrevStation = new long[number + 5];
        for (int i = 1; i < number; i++)
        {
            _calculateVar.Processed[i] = 0;
        }
    }

    /// <summary>
    /// 导入已经处理的邻接矩阵并使用迪杰斯特拉算法寻找最短路径(遍历)
    /// </summary>
    /// <param name="graphMatrix"></param>
    public DijkstraAlgorithmSeekPath(long[,] graphMatrix) : base(graphMatrix)
    {
        ConsoleHelper.Print("Initializing the Dijkstra algorithm...", ConsoleColor.Gray, 1);
        _calculateVar.Distance    = new long[GraphMatrix.GetLength(0)];
        _calculateVar.PrevPath    = new List<long>[GraphMatrix.GetLength(0)];
        _calculateVar.Processed   = new long[GraphMatrix.GetLength(0)];
        _calculateVar.PrevStation = new long[GraphMatrix.GetLength(0)];
        for (int i = 1; i < GraphMatrix.GetLength(0); i++)
        {
            _calculateVar.Processed[i] = 0;
        }

        ConsoleHelper.Print("The Dijkstra algorithm has been initialized.", ConsoleColor.Green, 1);
    }

    /// <summary>
    /// 寻找该节点到此图中所有其他节点的最小权重路径
    /// </summary>
    /// <param name="network"></param>
    /// <param name="startStation"></param>
    public void ShortestPath(Graph network, List<LineName> lineList, StationList startStation)
    {
        ConsoleHelper.Print("Start calculating...", ConsoleColor.Gray, 2);
        long shortestDistance;
        long shortestVertex = 1;
        int  i, j;
        long source = Convert.ToInt32(startStation.StationNo);
        ConsoleHelper.Print(
            "Traverse and record the reachable points and distances of the source(" + startStation.StationNameZh + "," +
            startStation.StationNameEn + ")...", ConsoleColor.Green, 2);
        ConsoleHelper.PrintInLine("No." + startStation.StationNo + "", ConsoleColor.Gray);
        for (i = 1; i < GraphMatrix.GetLength(0); i++)
        {
            _calculateVar.Distance[i] = GraphMatrix[source, i];
        }

        _calculateVar.Processed[source] = 1;
        _calculateVar.Distance[source]  = 0;
        ConsoleHelper.Print("Calculating shortest distance to all points...", ConsoleColor.Green, 2);

        #region 计算到所有节点的最短距离

        for (i = 1; i < GraphMatrix.GetLength(0) - 1; i++)
        {
            shortestDistance = INFINTE_NUM;
            for (j = 1; j < GraphMatrix.GetLength(0); j++) //记录可达中最近的点
            {
                if (shortestDistance > _calculateVar.Distance[j] && _calculateVar.Processed[j] == 0)
                {
                    shortestVertex   = j;
                    shortestDistance = _calculateVar.Distance[j];
                }
            }

            _calculateVar.Processed[shortestVertex] = 1;

            for (j = 1; j < GraphMatrix.GetLength(0); j++)
            {
                if (_calculateVar.Processed[j] == 0 &&
                    _calculateVar.Distance[shortestVertex] + GraphMatrix[shortestVertex, j] < _calculateVar.Distance[j])
                {
                    _calculateVar.Distance[j] = _calculateVar.Distance[shortestVertex] + GraphMatrix[shortestVertex, j];
                    _calculateVar.PrevStation[j] = shortestVertex;
                    ConsoleHelper.Print(
                        "Update the data of point " +
                        DataLanguageConverter.stationNo2StationList(j.ToString().PadLeft(4, '0'), network).First()
                                             .StationNameZh + "(No. " + j.ToString().PadLeft(4, '0') +
                        "), the shortest distance is " + _calculateVar.Distance[j] +
                        "m, and the backtracking sub-node is No." + _calculateVar.PrevStation[j] + ".",
                        ConsoleColor.Cyan, 3);
                }
                else if (_calculateVar.Processed[j] == 0 &&
                         _calculateVar.Distance[shortestVertex] + GraphMatrix[shortestVertex, j] ==
                         _calculateVar.Distance[j])
                {
                    _calculateVar.PrevStation[j] = shortestVertex;
                }
            }
        }

        ConsoleHelper.Print("Done calculate.", ConsoleColor.DarkGreen, 3);

        #endregion

        #region 遍历向原点回溯计算或导入回溯路径

        ConsoleHelper.Print("Calculating shortest path to all points...", ConsoleColor.Green, 2);
        for (j = 1; j < GraphMatrix.GetLength(0); j++)
        {
            if (_calculateVar.Distance[j] != INFINTE_NUM && _calculateVar.Distance[j] != 0) //改点有路径可达且距离不为0
            {
                ConsoleHelper.PrintInLineWithTimer(
                    "The path to " +
                    DataLanguageConverter.stationNo2StationList(j.ToString().PadLeft(4, '0'), network).First()
                                         .StationNameZh + " has not been calculated yet, now we start backtracking:",
                    ConsoleColor.DarkYellow, 3);
                Thread.Sleep(millisecondsTimeout: 1);
                long prevStationNo          = _calculateVar.PrevStation[j];
                int  newlyCalculatedNodeNum = 0;
                int  inportNodeNum          = 0;//不得不生成
                _calculateVar.PrevPath[j] = new List<long>();
                _calculateVar.PrevPath[j].Add(j); //二话不说先把自己加进去，然后再一站一站回溯
                if (prevStationNo != 0)
                    while (prevStationNo != source) //遍历回溯地铁网络，寻找上一站的上一站，直到上一站是出发地除非从起点站一站就到 
                    {
                        if (_calculateVar.PrevPath[prevStationNo] != null) //前一站已经计算过最短路
                        {
                            ConsoleHelper.Print(
                                "The previous node " +
                                DataLanguageConverter
                                    .stationNo2StationList(prevStationNo.ToString().PadLeft(4, '0'), network).First()
                                    .StationNameZh + "(Code " + prevStationNo.ToString().PadLeft(4, '0') +
                                ") has calculated path traceback data, abort the traceback and insert the traceback data.",
                                ConsoleColor.Blue, 4, RETURN);

                            //_calculateVar.PrevPath[j].Add(j);//不能加载此处，否则如果回溯的上一站没有计算过，而再回溯一站计算过（换乘），则会在新计算的路径和导入的已回溯路径间插入本站
                            _calculateVar.PrevPath[j]
                                         .AddRange(_calculateVar.PrevPath[prevStationNo]); //把前一站已经计算过的最短路径加入
                            inportNodeNum = _calculateVar.PrevPath[prevStationNo].Count;
                            break;
                        }

                        if (_calculateVar.PrevStation[prevStationNo] == 0) //回溯着，前一站就是出发地
                        {
                            //_calculateVar.PrevStation[prevStationNo] = source;//把前一站设置为起点，不break以将本站添加到path中，否则中间缺失一站
                            _calculateVar.PrevPath[j].Add(prevStationNo);
                            break;
                        }

                        ConsoleHelper.PrintInLine(prevStationNo.ToString().PadLeft(4, '0'), ConsoleColor.DarkBlue);
                        Thread.Sleep(20);
                        ConsoleHelper.PrintInLine(
                            "(" + DataLanguageConverter
                                  .stationNo2StationList(prevStationNo.ToString().PadLeft(4, '0'), network).First()
                                  .StationNameZh + ")", ConsoleColor.Gray);

                        //很不幸，上一站之前没有计算过，那么就只有再往出发地回溯一站了
                        _calculateVar.PrevPath[j].Add(prevStationNo);
                        prevStationNo = _calculateVar.PrevStation[prevStationNo]; //把回溯目标设为前一站
                        newlyCalculatedNodeNum++;
                    }

                if (_calculateVar.PrevPath[j].Last() != source) _calculateVar.PrevPath[j].Add(source); //加起点站
                if (newlyCalculatedNodeNum == 0)
                    ConsoleHelper.Print("Already back to the starting point!", ConsoleColor.Green, 4);
                else ConsoleHelper.Print("Already back to the starting point!", ConsoleColor.Green, 4, RETURN);
                ConsoleHelper.Print(
                    "New backtracking calculation " + newlyCalculatedNodeNum + " station, import " + inportNodeNum +
                    " stations.", ConsoleColor.Gray, 4);
            }
        }

        #endregion

        #region 遍历打印结果

        ConsoleHelper.Print("Shortest path and length results:", default, 1);
        for (j = 1; j < GraphMatrix.GetLength(0); j++)
        {
            if (_calculateVar.Distance[j] != INFINTE_NUM)
            {
                var toStation = DataLanguageConverter.stationNo2StationList(j.ToString().PadLeft(4, '0'), network);
                Thread.Sleep(1);
                ConsoleHelper.Print(DIVIDED_LINE_TEXT, ConsoleColor.Gray);
                ConsoleHelper.Print(
                    "The shortest distance between station " + startStation.StationNameZh + " and " +
                    toStation.First().StationNameZh + " is " + _calculateVar.Distance[j] + "m.", ConsoleColor.DarkCyan,
                    3);
                ConsoleHelper.Print("The shortest path is : ", ConsoleColor.Cyan, 4);
                if (_calculateVar.PrevPath[j] != null && _calculateVar.PrevPath[j].Count != 0)
                {
                    ConsoleHelper.PrintInLineWithTimer("", ConsoleColor.Cyan, 5);
                    _calculateVar.PrevPath[j].Reverse(); //逆转List路径
                    bool startTransFormChecking = false;
                    int  loopCheckIndex         = 1;
                    for (int n = 0; n < _calculateVar.PrevPath[j].Count; n++)
                    {
                        //TODO:把这逼玩意儿重写了，思路：结构体存储换乘站（在主函数就存），诉求：直到stationNo要直到本站的其它stationNo（如果是换乘站的话）
                        int intJ = Convert.ToInt16(j);
                        if (n + loopCheckIndex < _calculateVar.PrevPath[j].Count &&
                            DataLanguageConverter
                                .stationNo2StationList(_calculateVar.PrevPath[intJ][n].ToString().PadLeft(4, '0'),
                                                       network).First().StationNameZh ==
                            DataLanguageConverter
                                .stationNo2StationList(_calculateVar.PrevPath[intJ][n + 1].ToString().PadLeft(4, '0'),
                                                       network).First().StationNameZh) //如果下一站存在且当前站与下一站距离为0（换乘）
                        {
                            if (startTransFormChecking == true) loopCheckIndex++;
                            else startTransFormChecking = true;
                        }
                        else
                        {
                            if (startTransFormChecking == true) //上一个站是换乘站，这一个站就从换乘站走了
                            {
                                var transformFromStation = DataLanguageConverter.stationNo2StationList(
                                    _calculateVar.PrevPath[j][n - loopCheckIndex].ToString().PadLeft(4, '0'), network);
                                var stationName = transformFromStation.First().StationNameZh;
                                var transformToStation
                                    = DataLanguageConverter.stationNo2StationList(
                                        _calculateVar.PrevPath[j][n].ToString().PadLeft(4, '0'), network);
                                var transformFromLineName = DataLanguageConverter
                                                            .LineNo2LineName(
                                                                transformFromStation.First().StationCodes.First()
                                                                    .LineNo, lineList).Value.lineNameZh;
                                var transformToLineName = DataLanguageConverter
                                                          .LineNo2LineName(
                                                              transformToStation.First().StationCodes.First().LineNo,
                                                              lineList).Value.lineNameZh;
                                if (n + loopCheckIndex == _calculateVar.PrevPath[j].Count)
                                {
                                    ConsoleHelper.PrintInLine(
                                        stationName + "(" + transformFromLineName + "🔁" + transformToLineName + ")",
                                        ConsoleColor.DarkCyan);
                                    break;
                                }
                                else
                                {
                                    ConsoleHelper.PrintInLine(
                                        stationName + "(" + transformFromLineName + "🔁" + transformToLineName + ")-",
                                        ConsoleColor.DarkCyan);

                                    //n+=loopCheckIndex;//跳过下一站（毕竟是换乘站，和本站一样）
                                }

                                startTransFormChecking = false;
                                loopCheckIndex         = 1;
                            }
                            else
                            {
                                var station = DataLanguageConverter.stationNo2StationList(
                                    _calculateVar.PrevPath[j][n].ToString().PadLeft(4, '0'), network);
                                var stationName = station.First().StationNameZh;
                                if (n + 1 == _calculateVar.PrevPath[j].Count)
                                    ConsoleHelper.PrintInLine(stationName, ConsoleColor.Cyan);
                                else ConsoleHelper.PrintInLine(stationName + "-", ConsoleColor.Cyan);
                            }
                        }
                    }

                    /*foreach (var node in _calculateVar.PrevPath[j])
                    {
                        var station = DataLanguageConverter.stationNo2StationList(node.ToString().PadLeft(4, '0'), network);
                        ConsoleHelper.PrintInLine(station.First().StationNameZh + "-", ConsoleColor.Cyan);
                    }*/
                    ConsoleHelper.PrintInLine(RETURN, ConsoleColor.Cyan);
                }
                else if (_calculateVar.PrevPath[j] != null && _calculateVar.PrevPath[j].Count == 0) //一站就能到
                {
                    ConsoleHelper.Print(startStation.StationNameZh + "-" + toStation.First().StationNameZh,
                                        ConsoleColor.Cyan, 5);
                }
                else ConsoleHelper.Print("No available path.", ConsoleColor.DarkRed, 5); //出发点即为目的地
            }
        }

        #endregion
    }

    public static void StartDijkstra(Graph network, List<LineName> lineList, StationList station)
    {
        ConsoleHelper.Print(DIVIDED_LINE_TEXT, ConsoleColor.Green);
        ConsoleHelper.Print("Network built,now start dijkstra to find shortest path to every station.",
                            ConsoleColor.Green);
        ConsoleHelper.Print(DIVIDED_LINE_TEXT, ConsoleColor.Green);
        DijkstraAlgorithmSeekPath algorithmObj = new DijkstraAlgorithmSeekPath(network.edges);
        algorithmObj.ShortestPath(network, lineList, station);
    }
}