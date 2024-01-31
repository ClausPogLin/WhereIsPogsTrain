using System.Collections;
using System.Reflection;
using WhereIsPogsTrain.Models;
using static WhereIsPogsTrain.Data;

namespace WhereIsPogsTrain.Algorithm;

public class Graph
{
    public ArrayList vertexList;//存储点的链表
    public long[,] edges;//邻接矩阵，用来存储边
    private int numOfEdges;//边的数目
    
    public Graph(int n) {
        //初始化矩阵，一维数组，和边的数目
        edges=new long[n,n];
        vertexList=new ArrayList(n);
        ConsoleHelper.Print("Building edges graph...",default,1);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                edges.SetValue(INFINTE_NUM,i,j);
            }
        }
        ConsoleHelper.Print("The network diagram has been initialized with size of "+n+".",ConsoleColor.Green,1);
        numOfEdges=0;
    }
    
    //得到结点的个数
    public int getNumOfVertex() {
        return vertexList.Count;
    }
    
    //得到边的数目
    public int getNumOfEdges() {
        return numOfEdges;
    }
    
    //返回结点i的数据
    public Object getValueByIndex(int i) {
        return vertexList[i];
    }
    
    //返回v1,v2的权值
    public long getWeight(int v1,int v2) {
        return edges[v1,v2];
    }
    
    //插入结点
    public void insertVertex(Object vertex) {
        vertexList.Add(vertex);
    }
    
    //插入结点
    public void insertEdge(int v1,int v2,long weight) {
        edges[v1,v2]=weight;
        numOfEdges++;
    }
    
    //删除结点
    public void deleteEdge(int v1,int v2) {
        edges[v1,v2]=0;
        numOfEdges--;
    }
    
    //得到第一个邻接结点的下标
    public int getFirstNeighbor(int index) {
        for(int j=0;j<vertexList.Count;j++) {
            if (edges[index,j]>0) {
                return j;
            }
        }
        return -1;
    }
    
    //根据前一个邻接结点的下标来取得下一个邻接结点
    public int getNextNeighbor(int v1,int v2) {
        for (int j=v2+1;j<vertexList.Count;j++) {
            if (edges[v1,j]>0) {
                return j;
            }
        }
        return -1;
    }
}

public static class DataLanguageConverter
{
    public static List<StationList>? stationNo2StationList(string stationNo,Graph network)
    {
        var stationList = new List<StationList>();
        string stationName = "";
        foreach (StationList station in network.vertexList)
        {
            if (station.StationNo == stationNo)
            {
                stationList.Insert(0,station);
                if (station.StationCodes.Count == 1)
                {
                    return stationList;
                }
            }
        }
        foreach (StationList station in network.vertexList)
        {
            if (station.StationName == stationName && (stationNo != station.StationNo))
            {
                stationList.Add(station);
            }
        }

        if (stationList.Count == 0) return null;
        else return stationList;
    }

    public static List<LineName> oriData2LineList(List<Datum> data)
    {
        var lineName = new List<LineName>();
        foreach (var line in data)
        {
            lineName.Add(new LineName()
            {
                lineColorHex = line.LineColor,
                lineNameEn = line.LineNameEn,
                lineNameZh = line.LineNameZh,
                lineNo = line.LineNo
            });
        }
        return lineName;
    }

    public static LineName? LineNo2LineName(string lineNo,List<LineName> lineList)
    {
        foreach (var line in lineList)
        {
            if (lineNo == line.lineNo)
            {
                return line;
            }
        }

        return null;
    }
    
}