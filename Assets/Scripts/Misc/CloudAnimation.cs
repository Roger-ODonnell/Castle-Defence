using UnityEngine;

public class CloudAnimation : MonoBehaviour
{
    [Header("Vertical Motion")]
    public float verticalAmplitude = 0.5f;
    public float verticalSpeed = 0.5f;

    [Header("Horizontal Drift")]
    public float driftAmplitude = 0.5f;
    public float driftSpeed = 0.2f;

    private float startX;
    private float startY;
    private float seedX;
    private float seedY;

    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;

        // Give each cloud its own random noise seeds so they move uniquely
        seedX = Random.Range(0f, 9999f);
        seedY = Random.Range(0f, 9999f);
    }

    void Update()
    {
        float t = Time.time;

        // Smooth perlin-based vertical bobbing
        float newY = startY +
            (Mathf.PerlinNoise(seedY, t * verticalSpeed) - 0.5f) * verticalAmplitude * 2f;

        // Smooth perlin-based horizontal drift
        float newX = startX +
            (Mathf.PerlinNoise(seedX, t * driftSpeed) - 0.5f) * driftAmplitude * 2f;

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
