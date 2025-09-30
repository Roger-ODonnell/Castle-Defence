using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject entityPrefab;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] ParticleSystem spawnSmoke;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Instantiate(entityPrefab, transform.position, Quaternion.identity);
            if (spawnSmoke != null)
            {
                ParticleSystem smoke = Instantiate(spawnSmoke, transform.position, Quaternion.identity);
            }
            timer = 0f;
        }
    }
}
