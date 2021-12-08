using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private float playerMaxHealth = 10f;
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private Texture2D customCursor;
    [SerializeField] private TextMeshProUGUI currencyCounter;
    [SerializeField] private LevelLoader myLevelLoader;

    private float playerHealth;
    private int currency;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerHealth = playerMaxHealth;
        playerHealthBar.maxValue = playerMaxHealth;

        UpdateHealthbar(playerHealth);

        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    public void PlayerTakeDamage(float damage)
    {
        playerHealth -= damage;

        UpdateHealthbar(playerHealth);

        if (playerHealth <= 0)
        {
            PlayerDeath();
            return;
        }
    }

    private void UpdateHealthbar(float value)
    {
        playerHealthBar.value = value;
    }

    private void PlayerDeath()
    {
        myLevelLoader.LoadTargetScene(SceneManager.GetActiveScene().name);

        playerHealth = playerMaxHealth;
        UpdateHealthbar(playerHealth);
    }

    public void HideUIElements()
    {
        UICanvas.SetActive(false);
    }

    public void ShowUIElements()
    {
        UICanvas.SetActive(true);
    }

    public void GainCurrency(int amount)
    {
        currency += amount;

        UpdateCurrencyCounter();
    }

    public void LoseCurrency(int amount)
    {
        if (currency - amount <= 0)
        {
            currency = 0;
        }
        else
        {
            currency -= amount;
        }

        UpdateCurrencyCounter();
    }

    private void UpdateCurrencyCounter()
    {
        currencyCounter.SetText("{0}", currency);
    }
}
