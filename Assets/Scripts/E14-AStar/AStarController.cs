using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AStarController : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject player;

    float moveSpeed = 5f;

    // ���� ũ��
    int cellSize = 1;

    // ���� ����
    int numOfRows = 10;

    // ���� �ʺ�
    int numOfColumns = 10;

    // ��� ����
    Stack<AStarNode> pathList;

    // ��ֹ�
    GameObject[] obstacles;

    // ���� ���� ��ġ
    Vector3 origin = Vector3.zero;

    // AStarNode �迭
    AStarNode[,] nodes;

    // AStarController �̱���
    static AStarController instance = null;
    public static AStarController Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindAnyObjectByType(typeof(AStarController)) as AStarController;
                if (!instance)
                {
                    Debug.Log("AStarController�� ���� �������� �ʽ��ϴ�.");
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        // nodes �迭 �ʱ�ȭ
        InitNodes();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "plane")
                {
                    Vector3 targetPosition = hit.point;
                    targetPosition = new Vector3(Mathf.Floor(targetPosition.x), 
                        targetPosition.y, 
                        Mathf.Floor(targetPosition.z));

                    if (player != null)
                    {
                        // ���� Nodes�� ������ ����
                        ClearNodes();

                        (int playerRow, int playerCol) = GetNodeIndex(player.transform.position);
                        AStarNode startNode = nodes[playerRow, playerCol];

                        (int targetRow, int targetCol) = GetNodeIndex(targetPosition);
                        AStarNode endNode = nodes[targetRow, targetCol];

                        // AStar �˰��� ���
                        pathList = AStar.FindPath(startNode, endNode);
                    }
                }
            }
        }

        // Player �̵� ó��
        if (pathList != null && pathList.Count > 0)
        {
            AStarNode nextNode = pathList.Peek();
            Vector3 targetPosition = nextNode.position;

            if (player != null)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position,
                    targetPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(player.transform.position, targetPosition) < 0.001f)
                {
                    var popNode = pathList.Pop();
                }
            }
        }
    }

    void ClearNodes()
    {
        foreach (AStarNode node in nodes)
        {
            node.Clear();
        }
    }

    void InitNodes()
    {
        nodes = new AStarNode[numOfRows, numOfColumns];

        // Node�� ��ġ ���
        int index = 0;

        for (int i = 0; i < numOfRows; i++)
        {
            for (int j = 0; j < numOfColumns; j++)
            {
                AStarNode node = new AStarNode(new Vector3(j, 0, i), index);
                nodes[i, j] = node;
                index++;
            }
        }

        // ��ֹ� ��ġ ����
        if (obstacles != null && obstacles.Length > 0)
        {
            foreach (GameObject obstacle in obstacles)
            {
                var obstaclePositionIndexes = GetNodeIndex(obstacle.transform.position);
                nodes[obstaclePositionIndexes.row, obstaclePositionIndexes.column].isObstacle = true;
            }
        }
    }

    (int row, int column) GetNodeIndex(Vector3 position)
    {
        int columnIndex = (int)Mathf.Round(position.x) / cellSize;
        int rowIndex = (int)Mathf.Round(position.z) / cellSize;

        return (rowIndex, columnIndex);
    }

    // �־��� Node�� �̿� Node���� ��ȯ�ϴ� �޼���
    // �̵� ������ Node���� Ȯ���Ͽ� ��ȯ
    public ArrayList GetAvailableNodes(AStarNode node)
    {
        (int rowAdder, int colAdder)[] indexAdder = { (1, 0), (-1, 0), (0, 1), (0, -1) };

        ArrayList resultList = new ArrayList();
        Vector3 nodePosition = node.position; ;
        var nodePositionIndex = GetNodeIndex(nodePosition);

        int nodeRowIndex;
        int nodeColumnIndex;

        foreach (var adder in indexAdder)
        {
            nodeRowIndex = nodePositionIndex.row + adder.rowAdder;
            nodeColumnIndex = nodePositionIndex.column + adder.colAdder;

            if (IsAvailableNode(nodeRowIndex, nodeColumnIndex))
            {
                resultList.Add(nodes[nodeRowIndex, nodeColumnIndex]);
            }
        }

        return resultList;
    }

    bool IsAvailableNode(int rowIndex, int columnIndex)
    {
        // �ش� row, column�� ��ȿ���� ������ false
        if (!IsAvailableIndex(rowIndex, columnIndex)) return false;

        // �ش� row, column�� ��ֹ��� �ƴ��� Ȯ��
        AStarNode node = nodes[rowIndex, columnIndex];
        if (!node.isObstacle)
        {
            return true;
        }
        return false;
    }

    bool IsAvailableIndex(int rowIndex, int columnIndex)
    {
        if (rowIndex > -1 && columnIndex > -1 && rowIndex < numOfRows && columnIndex < numOfColumns)
        {
            return true;
        }
        return false;
    }
}
