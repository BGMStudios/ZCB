using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    public GameObject buildingPrefab;

    public Building buildingToBuild;
    public bool isBuilding;
    public bool canPlaceBuilding;

    public List<string> errorMessages;
    public BuildingError currentError;

    private Building currentBuilding;

    public delegate void OnBeginBuild_D();
    public delegate void OnEndBuild_D();
    public delegate void OnBuildError_D(BuildingError error);
    public event OnBeginBuild_D OnBeginBuild;
    public event OnBeginBuild_D OnEndBuild;
    public event OnBuildError_D OnBuildError;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);

        OnBeginBuild += BeginBuilding;
        OnEndBuild += EndBuilding;
    }

    public void SetBuilding(Building building)
    {
        buildingToBuild = building;
    }

    private void BeginBuilding()
    {
        isBuilding = true;

        currentBuilding = Instantiate(buildingPrefab, transform.position, Quaternion.identity).GetComponent<Building>();
        currentBuilding.isBeingBuilt = true;
        currentBuilding.transform.position = GameManager.Instance.selectionBox.transform.position + new Vector3(.5f * (currentBuilding.size.x - 1), .5f * (currentBuilding.size.y - 1));

        currentBuilding.gameObject.layer = LayerMask.NameToLayer("Pending Building");

        currentBuilding.state = BuildingState.InConstruction;
        currentBuilding.UpdateGraphics();
    }

    private void EndBuilding()
    {
        if (CheckValidity())
        {
            isBuilding = false;

            currentBuilding.isBeingBuilt = false;
            currentBuilding.state = BuildingState.Normal;
            currentBuilding.gameObject.layer = LayerMask.NameToLayer("Building");
            currentBuilding.UpdateGraphics();
            currentBuilding.buildingFunction.OnBuilt();
        }
    }

    public void RaiseError(BuildingError error)
    {
        Debug.Log(errorMessages[(int)error]);
        OnBuildError?.Invoke(error);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePendingBuilding();

        // DEBUG
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!isBuilding)
                OnBeginBuild?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isBuilding)
                OnEndBuild?.Invoke();
        }
    }

    void MovePendingBuilding()
    {
        if (isBuilding)
        {
            currentBuilding.transform.position = GameManager.Instance.selectionBox.transform.position + new Vector3(.5f * (currentBuilding.size.x - 1), .5f * (currentBuilding.size.y - 1));
        }
    }

    bool CheckValidity()
    {
        Vector2 size = currentBuilding.boxCollider.size - Vector2.one * .05f;

        if (Physics2D.BoxCast(currentBuilding.transform.position, size, 0, Vector2.zero, 1, LayerMask.GetMask("Building")))
        {
            RaiseError(BuildingError.SpaceOccupied);
            return false;
        }

        if (!currentBuilding.canBePlacedOnWater && Physics2D.BoxCast(currentBuilding.transform.position, size, 0, Vector2.zero, 1, LayerMask.GetMask("Water")))
        {
            RaiseError(BuildingError.CantPlaceOnWater);
            return false;
        }

        if (currentBuilding.coastOnly && !Physics2D.BoxCast(currentBuilding.transform.position, size, 0, Vector2.zero, 1, LayerMask.GetMask("Coast")))
        {
            RaiseError(BuildingError.NeedsWater);
            return false;
        }

        return true;
    }
}
