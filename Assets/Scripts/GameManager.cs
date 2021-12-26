using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isInputEnabled = true;

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private int playerMaxHealth = 500;

    [SerializeField] private Slider playerSanityBar;
    [SerializeField] private int playerMaxSanity = 200;

    [SerializeField] private Slider playerStaminaBar;
    [SerializeField] private int playerMaxStamina = 100;
    [SerializeField] private float staminaRegenerationRate = 1f;

    [SerializeField] private GameObject UICanvas;
    [SerializeField] private Texture2D customCursor;
    [SerializeField] private TextMeshProUGUI currencyCounter;
    [SerializeField] private LevelLoader myLevelLoader;

    private int playerHealth;
    private int playerSanity;
    private int playerStamina;
    private bool staminaRegenerationActive = true;
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

        playerSanity = playerMaxSanity;
        playerSanityBar.maxValue = playerMaxSanity;

        playerStamina = playerMaxStamina;
        playerStaminaBar.maxValue = playerMaxStamina;

        UpdateHealthbar();
        UpdateSanityBar();
        UpdateStaminaBar();

        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;

        UpdateHealthbar();

        if (playerHealth <= 0)
        {
            PlayerDeath();
            return;
        }
    }

    private void UpdateHealthbar()
    {
        playerHealthBar.value = playerHealth;
    }

    private void PlayerDeath()
    {
        myLevelLoader.LoadTargetScene(SceneManager.GetActiveScene().name);

        playerHealth = playerMaxHealth;
        UpdateHealthbar();
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

    public bool CanAffordStaminaCost(int staminaCost)
    {
        return (playerStamina - staminaCost) >= 0;
    }

    public void ReduceStamina(int staminaCost)
    {
        playerStamina -= staminaCost;
        UpdateStaminaBar();
    }

    public void StopStaminaRegeneration()
    {
        staminaRegenerationActive = false;
        StopCoroutine("RegenerateStamina");
    }

    public void StartStaminaRegeneration()
    {
        if (!staminaRegenerationActive)
        {
            StartCoroutine("RegenerateStamina");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartStaminaRegeneration();
    }

    private void UpdateStaminaBar()
    {
        playerStaminaBar.value = playerStamina;
    }

    private IEnumerator RegenerateStamina()
    {
        staminaRegenerationActive = true;

        yield return new WaitForSeconds(1f);

        while (playerStamina < playerMaxStamina)
        {
            playerStamina++;
            UpdateStaminaBar();

            yield return new WaitForSeconds(0.03f / staminaRegenerationRate);
        }

        staminaRegenerationActive = false;
    }

    private void UpdateSanityBar()
    {
        playerSanityBar.value = playerSanity;
    }
}
