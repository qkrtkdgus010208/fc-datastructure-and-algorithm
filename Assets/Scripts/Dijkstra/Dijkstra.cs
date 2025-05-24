using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra<T>
{
    public List<WeightedNode<T>> nodes;

    public Dijkstra() 
    { 
        nodes = new List<WeightedNode<T>>();
    }

    public void AddNode(WeightedNode<T> node)
    {
        nodes.Add(node);
    }

    public void AddEdge(WeightedNode<T> firstNode, WeightedNode<T> secondNode, int distance) 
    {
        firstNode.AddNeighborNode(secondNode, distance);
        secondNode.AddNeighborNode(firstNode, distance);
    }

    public void StartDijkstra(WeightedNode<T> startNode, WeightedNode<T> endNode)
    {
        Dictionary<WeightedNode<T>, int> distance = new Dictionary<WeightedNode<T>, int>();
        Dictionary<WeightedNode<T>, WeightedNode<T>> previousNode = new Dictionary<WeightedNode<T>, WeightedNode<T>>();
        List<WeightedNode<T>> unvisited = new List<WeightedNode<T>>();

        foreach (var node in nodes)
        {
            if (node == startNode)
            {
                distance[node] = 0;
            }
            else
            {
                distance[node] = int.MaxValue;
            }
            unvisited.Add(node);
        }

        while (unvisited.Count > 0)
        {
            WeightedNode<T> node = null;

            // unvisited ���� Ž���ؼ� distance�� nvisitedNode�� ����ġ��
            // node�� �Ÿ����� ���� ���� ã���ϴ�.
            // ó������ startNode�� distance�� 0�̱� ������ startNode�� node�� �˴ϴ�.
            foreach (var possibleNode in unvisited)
            {
                if (node == null || distance[possibleNode] < distance[node])
                {
                    node = possibleNode;
                }
            }

            unvisited.Remove(node);

            foreach (var neighbor in node.neighbors)
            {
                // node�� neighbors�� Ž���ؼ� �ش� ��尡 unvisied ����Ʈ�� ���ٸ� continue �մϴ�.
                if (unvisited.Contains(neighbor.targetNode) == false)
                {
                    continue;
                }

                // neighbor node�� ���� unvisited�� �ִٸ�
                // node�� distance ���� neighbor�� distance ���� ���ؼ� ������ �����մϴ�.
                // alt ���� ���� distance �迭�� ����� �� ���� ������ distance �迭�� ���� ������Ʈ �մϴ�.
                // �ƿ﷯ �ش� ���� ������Ʈ �� ���� ����� ������ �����ϸ� �ִ� ��θ� ���� �� �ֽ��ϴ�.
                int alt = distance[node] + neighbor.distance;
                if (alt < distance[neighbor.targetNode])
                {
                    distance[neighbor.targetNode] = alt;
                    previousNode[neighbor.targetNode] = node;
                }
            }
        }

        // ��� ���
        List<WeightedNode<T>> pathList = new List<WeightedNode<T>>();

        WeightedNode<T> currentKey = endNode;

        while (previousNode.ContainsKey(currentKey))
        {
            currentKey = previousNode[currentKey];
            pathList.Add(currentKey);
        }

        pathList.Reverse();

        foreach (var path in pathList)
        {
            Debug.Log(path.data.ToString());
        }
    }
}
