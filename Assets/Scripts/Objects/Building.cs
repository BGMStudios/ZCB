using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Vector2Int size;

    public BuildingState state;
    public bool isBeingBuilt;

    public bool canBePlacedOnWater;
    public bool coastOnly;

    [Header("Common Properties")]
    public Color normalColor;
    public Color buildingColor, invalidColor;

    private SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    public BuildingFunction buildingFunction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        buildingFunction = GetComponent<BuildingFunction>();

        boxCollider.size = size;
    }

    public void UpdateGraphics()
    {
        switch (state)
        {
            case BuildingState.InConstruction:
                spriteRenderer.color = buildingColor;
                break;
            case BuildingState.Normal:
                spriteRenderer.color = normalColor;
                break;
            case BuildingState.Invalid:
                spriteRenderer.color = invalidColor;
                break;
        }
    }
}
