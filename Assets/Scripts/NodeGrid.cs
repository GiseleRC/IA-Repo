using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] List<Node> nodeList = new List<Node>();

    public List<Node> GetNeighboursNodes(int node)
    {
        List<Node> neighbours = new List<Node>();

        return neighbours;
    }
}