using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSpawnableObject", menuName = "Castle Defence/Spawnable Object")]
public class SpawnableObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite icon;
    public string objectName;
    public float cost;
}
