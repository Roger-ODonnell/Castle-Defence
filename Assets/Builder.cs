using UnityEngine;

public class Builder : MonoBehaviour
{
    [Header("Placement Settings")]
    public Camera playerCamera;           // Main camera for raycasting
    public GameObject[] placeablePrefabs; // Array of defenses you can place
    public LayerMask terrainMask;         // Only raycast against terrain

    [Header("Preview Settings")]
    public Material previewMaterial;      // Semi-transparent material for preview

    private GameObject currentPreview;    // The ghost object
    private GameObject currentPrefab;     // The prefab to place
    private int selectedIndex = 0;

    bool prefabSpawned = false;

    // void Start()
    // {
    //     // Auto-select the first prefab if available
    //     if (placeablePrefabs.Length > 0)
    //     {
    //         SelectPrefab(0);
    //     }
    //     else
    //     {
    //         Debug.LogWarning("No prefabs assigned to placeablePrefabs array!");
    //     }
    // }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (placeablePrefabs.Length > 0 && !prefabSpawned)
            {
                SelectPrefab(0);
                prefabSpawned = true;
            }
            else if (prefabSpawned)
            {
                prefabSpawned = false;
                if (currentPreview != null)
                {
                    Destroy(currentPreview);
                }
                currentPrefab = null;
            }
        }

        HandlePlacement();
        HandleSwitching();
    }

    void HandlePlacement()
    {
        // Ray from mouse to terrain
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 500f, terrainMask))
        {
            if (currentPreview != null)
            {
                currentPreview.transform.position = hit.point;
            }

            if (Input.GetMouseButtonDown(0)) // Left click = place
            {
                PlaceObject(hit.point);
                Debug.DrawRay(hit.point, Vector3.up, Color.red, 1f);
            }
        }
    }

    void HandleSwitching()
    {
        // Scroll wheel to cycle defenses
        if (Input.mouseScrollDelta.y != 0 && placeablePrefabs.Length > 0)
        {
            selectedIndex += (Input.mouseScrollDelta.y > 0) ? 1 : -1;
            if (selectedIndex < 0) selectedIndex = placeablePrefabs.Length - 1;
            if (selectedIndex >= placeablePrefabs.Length) selectedIndex = 0;

            SelectPrefab(selectedIndex);
        }
    }

    void SelectPrefab(int index)
    {
        currentPrefab = placeablePrefabs[index];

        if (currentPreview != null)
        {
            Destroy(currentPreview);
        }

        if (currentPrefab != null)
        {
            // Create preview object
            currentPreview = Instantiate(currentPrefab);
            foreach (var r in currentPreview.GetComponentsInChildren<Renderer>())
            {
                r.material = previewMaterial; // Apply ghost material
            }
            currentPreview.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            Debug.LogWarning($"Prefab at index {index} is null!");
        }
    }

    void PlaceObject(Vector3 position)
    {
        if (currentPrefab == null)
        {
            Debug.LogWarning("No prefab selected to place!");
            return;
        }

        Instantiate(currentPrefab, position, Quaternion.identity);
    }
}
