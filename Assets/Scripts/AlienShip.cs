using UnityEngine;

public class AlienShip : MonoBehaviour
{
    AudioSource alienHover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alienHover = GetComponent<AudioSource>();
        alienHover.Play();
    }
}
