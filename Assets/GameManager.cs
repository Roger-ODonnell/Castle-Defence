using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject spawner;
    private GameObject startButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawner.SetActive(false);
        startButton = GameObject.FindGameObjectWithTag("StartBtn");

    }

    public void StartSpawner()
    {
        spawner.SetActive(true);
        startButton.SetActive(false);
    }


}
