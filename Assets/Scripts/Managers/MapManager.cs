using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public RoadGraph roadGraph;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);

        roadGraph = new RoadGraph();
        roadGraph.Init();
    }

    public void OnDrawGizmosSelected()
    {
        if (roadGraph != null)
        {
            foreach (RoadNode node in roadGraph.graph.Values)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(node.transform.position + Vector3.forward * -2f, Vector3.one * 0.2f);

                Gizmos.color = Color.red;

                foreach (RoadConnection connection in node.connections.Values)
                {
                    if (connection == null) continue;

                    Gizmos.DrawLine(connection.from.transform.position, connection.to.transform.position);
                }

            }
        }
        
    }
}
