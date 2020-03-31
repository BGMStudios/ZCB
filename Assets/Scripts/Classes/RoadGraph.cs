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
}