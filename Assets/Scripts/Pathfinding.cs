using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public List<Node> AStar(Node StartingNode, Node GoalNode)
    {
        if (StartingNode == null || GoalNode == null) return new List<Node>();

        PriorityQueue<Node> frontier = new PriorityQueue<Node>();
        frontier.Enqueue(StartingNode, 0);

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        cameFrom.Add(StartingNode, null);

        Dictionary<Node, int> costSoFar = new Dictionary<Node, int>();
        costSoFar.Add(StartingNode, 0);

        while (frontier.count > 0)
        {
            Node current = frontier.Dequeue();

            if (current == GoalNode)
            {
                List<Node> path = new List<Node>();
                while (current != StartingNode)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }

                path.Add(current); // Agregado para que no atraviese paredes

                path.Reverse();
                return path;
            }

            foreach (var next in current.GetNeightbours())
            {
                int newCost = costSoFar[current]; //+next.cost
                if (!costSoFar.ContainsKey(next))
                {
                    float priority = newCost + Vector3.Distance(next.transform.position, GoalNode.transform.position);
                    frontier.Enqueue(next, priority);
                    cameFrom.Add(next, current);
                    costSoFar.Add(next, newCost);
                }
                else if (costSoFar[next] > newCost)
                {
                    float priority = newCost + Vector3.Distance(next.transform.position, GoalNode.transform.position);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                    costSoFar[next] = newCost;
                }
            }
        }

        return new List<Node>();

    }
}
