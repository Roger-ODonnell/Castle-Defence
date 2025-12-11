using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject spawner;
    private GameObject startButton;
    public float playerMoney = 200;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] Animator startButtonAnim;
    [SerializeField] GameObject moneyUpdateText;
    [SerializeField] Transform moneyChangeSpawnPos;

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
        UpdateMoneyText(0);
    }

    public void StartSpawner()
    {
        spawner.SetActive(true);
        startButtonAnim.SetBool("Start", true);
        startButton.SetActive(false);
    }

    public async void UpdateMoneyText(float cost)
    {
        await changeMoney(cost);
    }

    public void moneyChange(float money)
    {
        GameObject moneyChangeText = Instantiate(moneyUpdateText, moneyChangeSpawnPos.position, quaternion.identity);
        moneyChangeText.transform.SetParent(moneyChangeSpawnPos);
        moneyChangeText.GetComponentInChildren<TMP_Text>().text = "$" + money.ToString();
        moneyChangeText.GetComponentInChildren<TMP_Text>().color = Color.red;
        Destroy(moneyChangeText, 1f);
    }

    async Task changeMoney(float cost)
    {
        AudioManager.Instance.PlaySFX("MoneyChangeMp3");
        moneyChange(cost);
        await Task.Delay(1000);  // simulate waiting
        playerMoney -= cost;
        moneyText.text = playerMoney.ToString();
    }


}
