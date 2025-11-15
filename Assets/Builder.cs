
using UnityEditor;
using UnityEngine;

public class Builder : MonoBehaviour
{
    [Header("Placement Settings")]
    public Camera playerCamera;           // Main camera for raycasting
    public GameObject[] placeablePrefabs; // Array of defenses you can place
    public LayerMask terrainMask;         // Only raycast against terrain


    [Header("Preview Settings")]
    public Material previewMaterial;      // Semi-transparent material for preview

    public GameObject currentPreview;    // The ghost object
    public GameObject currentPrefab;     // The prefab to place

    [Header("Collision Settings")]
    public LayerMask placementCheckMask; // assign this to "Placeables" layer in Inspector

    void Update()
    {
        HandlePlacement();

        if (Input.GetKey(KeyCode.Escape))
        {
            CancelChoice();
        }
    }

    public void PopulatePreview()
    {
        if (currentPrefab == null) return;

        currentPreview = Instantiate(currentPrefab, transform.position, Quaternion.identity);
        ApplyPreviewMaterial(currentPreview);
        currentPreview.layer = LayerMask.NameToLayer("Preview");
        foreach (Transform child in currentPreview.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = LayerMask.NameToLayer("Preview");
        }

    }

    void ApplyPreviewMaterial(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.material = previewMaterial;
        }
    }


    private void CancelChoice()
    {
        Destroy(currentPreview);
        currentPreview = null;
        currentPrefab = null;
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


    void PlaceObject(Vector3 position)
    {
        if (currentPrefab == null) return;

        Collider prefabCollider = currentPrefab.GetComponentInChildren<Collider>();
        if (prefabCollider == null)
        {
            Debug.LogWarning("Prefab has no collider for placement check!");
            return;
        }

        // Calculate box bounds for overlap check
        Vector3 prefabSize = prefabCollider.bounds.size;
        Vector3 halfExtents = prefabSize / 2f;
        Vector3 boxCenter = position + Vector3.up * halfExtents.y;

        // Check for overlapping colliders (but ignore triggers)
        Collider[] colliders = Physics.OverlapBox(boxCenter, halfExtents, Quaternion.identity, placementCheckMask);
        foreach (Collider col in colliders)
        {
            if (!col.isTrigger) // only block if collider is NOT a trigger
            {
                Debug.Log("Cannot place: space is occupied.");
                return;
            }
        }

        // Safe to place
        GameObject placedObject = Instantiate(currentPrefab, position, Quaternion.identity);
        placedObject.tag = "Buildable";
        placedObject.layer = LayerMask.NameToLayer("Placeables");
        Destroy(currentPreview);
        currentPreview = null;
        currentPrefab = null;
    }
}
