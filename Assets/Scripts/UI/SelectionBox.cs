using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Utils.SnapToGrid2D(mousePos, -1);
    }
}
