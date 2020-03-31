using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Utils
{
    public static Vector3 SnapToGrid(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x - .5f) + .5f, Mathf.Round(v.y - .5f) + .5f, Mathf.Round(v.z - .5f) + .5f);
    }

    public static Vector3 SnapToGrid2D(Vector3 v, float z)
    {
        return new Vector3(Mathf.Round(v.x - .5f) + .5f, Mathf.Round(v.y - .5f) + .5f, z);
    }
}
