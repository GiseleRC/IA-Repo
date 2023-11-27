using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] List<Node> nodeList = new List<Node>();

    public List<Node> GetNeightboursNodes(int node)
    {
        List<Node> neightnours = new List<Node>();

        return neightnours;
    }
}
