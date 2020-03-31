using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBox : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(Mathf.Round(mousePos.x - .5f) + .5f, Mathf.Round(mousePos.y - .5f) + .5f, -1);
    }
}
