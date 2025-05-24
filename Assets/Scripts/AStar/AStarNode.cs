using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode : IComparable
{
    public int index;
    public float hScore;    // ���� ��忡�� ��ǥ �������� ����ġ(�Ÿ�)
    public float gScore;    // ��� ��忡�� ���� �������� ����ġ(�Ÿ�)
    public float fScroe
    {
        get
        {
            return hScore + gScore;
        }
    }
    public Vector3 position;
    public bool isObstacle;
    public AStarNode parent;

    public AStarNode(Vector3 position, int index)
    {
        this.hScore = 0f;
        this.gScore = 0f;
        this.isObstacle = false;
        this.parent = null;
        this.position = position;
        this.index = index;
    }

    public void Clear()
    {
        this.hScore = 0f;
        this.gScore = 0f;
        this.parent = null;
    }

    public int CompareTo(object obj)
    {
        AStarNode node = obj as AStarNode;

        if (node != null)
        {
            if (this.fScroe < node.fScroe)
            {
                return -1;
            }
            else if (this.fScroe > node.fScroe)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        return 0;
    }
}
