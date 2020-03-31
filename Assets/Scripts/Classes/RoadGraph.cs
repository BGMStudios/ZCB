using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RoadGraph
{
    public Dictionary<Vector2, RoadNode> graph;

    public void Init()
    {
        graph = new Dictionary<Vector2, RoadNode>();
    }

    public RoadNode At(Vector2 pos)
    {
        return graph[pos];
    }

    public void Connect(RoadNode a, RoadNode b)
    {
        Vector2 deltaPos = a.transform.position - b.transform.position;
        deltaPos = new Vector2(Mathf.RoundToInt(deltaPos.x), Mathf.RoundToInt(deltaPos.y));

        if (!Mathf.Approximately(deltaPos.magnitude, 1)) return;

        if (deltaPos == Vector2.up)
        {
            a.ConnectWith(b, Direction.South);
            b.ConnectWith(a, Direction.North);
        }
        else if (deltaPos == Vector2.down)
        {
            a.ConnectWith(b, Direction.North);
            b.ConnectWith(a, Direction.South);
        }
        else if (deltaPos == Vector2.left)
        {
            a.ConnectWith(b, Direction.East);
            b.ConnectWith(a, Direction.West);
        }
        else if (deltaPos == Vector2.right)
        {
            a.ConnectWith(b, Direction.West);
            b.ConnectWith(a, Direction.East);
        }
    }

    List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 current)
    {
        List<Vector2> totalPath = new List<Vector2> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }

        return totalPath;
    }

    public delegate float AStarHeuristic(Vector2 source, Vector2 goal);

    public List<Vector2> Pathfind(Vector2 source, Vector2 goal, AStarHeuristic Heuristic)
    {
        // Simple implementation of A*

        List<Vector2> openSet = new List<Vector2> { source };
        List<Vector2> closedSet = new List<Vector2>();

        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();

        Dictionary<Vector2, float> gScores = new Dictionary<Vector2, float>();
        Dictionary<Vector2, float> fScores = new Dictionary<Vector2, float>();

        foreach (RoadNode level in graph.Values)
        {
            gScores[level.transform.position] = Mathf.Infinity;
            fScores[level.transform.position] = Mathf.Infinity;
        }
        gScores[source] = 0;
        fScores[source] = Heuristic(source, goal);

        while (openSet.Count != 0)
        {
            // Find level from the open set which has lowest fScore
            Vector2 currentNode = openSet[0];
            for (int i = 0; i < openSet.Count; i++)
            {
                if (fScores[currentNode] > fScores[openSet[i]])
                {
                    currentNode = openSet[i];
                }
            }

            if (currentNode == goal)
                return ReconstructPath(cameFrom, currentNode);

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // Get levels connected to this level
            List<Vector2> neighbors = new List<Vector2>();
            foreach (RoadConnection connection in graph[currentNode].connections.Values)
                if (connection != null) neighbors.Add(connection.to.transform.position);

            foreach (Vector2 neighbor in neighbors)
            {
                if (closedSet.Contains(neighbor)) continue;

                float tentativeGScore = gScores[currentNode] + 1;
                if (!openSet.Contains(neighbor)) openSet.Add(neighbor);
                else if (tentativeGScore >= gScores[neighbor]) continue;

                cameFrom[neighbor] = currentNode;
                gScores[neighbor] = tentativeGScore;
                fScores[neighbor] = gScores[neighbor] + Heuristic(neighbor, goal);
            }
        }
        return null;
    }
}