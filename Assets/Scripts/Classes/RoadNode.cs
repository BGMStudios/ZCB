using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RoadNode : MonoBehaviour
{
    public Dictionary<Direction, RoadConnection> connections;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        connections = new Dictionary<Direction, RoadConnection>();
    }

    public void ConnectWith(RoadNode node, Direction dir)
    {
        connections[dir] = new RoadConnection()
        {
            from = this,
            to = node
        };
    }
}
