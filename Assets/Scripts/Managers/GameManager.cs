using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Camera mainCamera;
    public SelectionBox selectionBox;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(gameObject);

        mainCamera = Camera.main;

        FindGameObjects();
    }

    private void FindGameObjects()
    {
        selectionBox = GameObject.FindGameObjectWithTag("Selection Box").GetComponent<SelectionBox>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
