using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject spawner;
    private GameObject startButton;
    public float playerMoney = 200;
    [SerializeField] private TMP_Text moneyText;

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
        UpdateMoneyText();
    }

    public void StartSpawner()
    {
        spawner.SetActive(true);
        startButton.SetActive(false);
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "€" + playerMoney;
    }


}
