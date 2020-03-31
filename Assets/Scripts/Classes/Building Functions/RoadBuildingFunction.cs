using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RoadBuildingFunction : BuildingFunction
{
    RoadNode node;

    private void Awake()
    {
        node = GetComponent<RoadNode>();
    }

    public override void OnBuilt()
    {
        MapManager.Instance.roadGraph.graph[transform.position] = node;

        if (MapManager.Instance.firstNode == null) MapManager.Instance.firstNode = node;
        MapManager.Instance.lastNode = node;

        for (int i = 0; i < 4; i++)
        {
            Vector2 raycastDir;
            switch ((Direction)i)
            {
                case Direction.North:
                    raycastDir = Vector2.up;
                    break;
                case Direction.West:
                    raycastDir = Vector2.left;
                    break;
                case Direction.South:
                    raycastDir = Vector2.down;
                    break;
                case Direction.East:
                    raycastDir = Vector2.right;
                    break;
                default:
                    raycastDir = Vector2.up;
                    break;
            }

            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDir, 1f, LayerMask.GetMask("Road"));
            gameObject.layer = LayerMask.NameToLayer("Road");

            if (hit.collider != null)
            {
                MapManager.Instance.roadGraph.Connect(node, hit.collider.GetComponent<RoadNode>());
            }
        }
    }
}
