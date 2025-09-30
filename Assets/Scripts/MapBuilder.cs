using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] GameObject[] Tiles;
    [SerializeField] int tileNum = 0;

    Rigidbody rb;

    Vector3 mousePos;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        if (Input.GetMouseButtonDown(0))
        {
            GameObject ActiveTile = Instantiate(Tiles[tileNum], mousePos, quaternion.identity);
           rb =  ActiveTile.GetComponent<Rigidbody>();
        }

        if (rb)
        {
            rb.position = mousePos;
        }
    }
}
