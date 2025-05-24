
using System.Collections.Generic;
using UnityEngine;

public class AVLTree<T>
{
    public BinaryTreeNode<T> Root { get; set; }

    public int NodeHeight(BinaryTreeNode<T> node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.Height;
    }

    public int GetBalanceFactor(BinaryTreeNode<T> node)
    {
        if (node == null)
        {
            return 0;
        }
        return NodeHeight(node.LeftNode) - NodeHeight(node.RightNode);
    }

    public void Insert(T value)
    {
        Root = Insert(Root, value);
    }

    public BinaryTreeNode<T> Insert(BinaryTreeNode<T> node, T value) 
    {
        if (node == null)
        {
            return new BinaryTreeNode<T>(value);
        }

        if (Comparer<T>.Default.Compare(value, node.Value) < 0) 
        { 
            node.LeftNode = Insert(node.LeftNode, value);
        }
        else if (Comparer<T>.Default.Compare(value, node.Value) > 0)
        {
            node.RightNode = Insert(node.RightNode, value);
        }
        else
        {
            return node;
        }

        // ���� ������Ʈ
        node.Height = 1 + Mathf.Max(NodeHeight(node.LeftNode), NodeHeight(node.RightNode));

        // ���� �μ� ���
        int balanceFactor = GetBalanceFactor(node);

        // �ұ��� ����
        // LL Case
        if (balanceFactor > 1 && Comparer<T>.Default.Compare(value, node.LeftNode.Value) < 0)
        {
            // ���������� ȸ��
            return RotateRight(node);
        }

        // RR Case
        if (balanceFactor < -1 && Comparer<T>.Default.Compare(value, node.RightNode.Value) > 0)
        {
            // �������� ȸ��
            return RotateLeft(node);
        }

        // LR Case
        if (balanceFactor > 1 && Comparer<T>.Default.Compare(value, node.LeftNode.Value) > 0)
        {
            // �������� ȸ��
            // ���������� ȸ��
            node.LeftNode = RotateLeft(node.LeftNode);
            return RotateRight(node);
        }

        // RL Case
        if (balanceFactor < -1 && Comparer<T>.Default.Compare(value, node.RightNode.Value) < 0)
        {
            // ���������� ȸ��
            // �������� ȸ��
            node.RightNode = RotateRight(node.RightNode);
            return RotateLeft(node);
        }

        return node;
    }

    // ������ ȸ��
    BinaryTreeNode<T> RotateRight(BinaryTreeNode<T> node)
    {
        BinaryTreeNode<T> x = node.LeftNode;
        BinaryTreeNode<T> T2 = x.RightNode;

        // ȸ��
        x.RightNode = node;
        node.LeftNode = T2;

        // ���� ������Ʈ
        node.Height = 1 + Mathf.Max(NodeHeight(node.LeftNode), NodeHeight(node.RightNode));
        x.Height = 1 + Mathf.Max(NodeHeight(x.LeftNode), NodeHeight(x.RightNode));

        // ���ο� ��Ʈ ��� ��ȯ
        return x;
    }

    // ���� ȸ��
    BinaryTreeNode<T> RotateLeft(BinaryTreeNode<T> node)
    {
        BinaryTreeNode<T> y = node.RightNode;
        BinaryTreeNode<T> T2 = y.LeftNode;

        // ȸ��
        y.LeftNode = node;
        node.RightNode = T2;

        // ���� ������Ʈ
        node.Height = 1 + Mathf.Max(NodeHeight(node.LeftNode), NodeHeight(node.RightNode));
        y.Height = 1 + Mathf.Max(NodeHeight(y.LeftNode), NodeHeight(y.RightNode));

        // ���ο� ��Ʈ ��� ��ȯ
        return y;
    }
}
