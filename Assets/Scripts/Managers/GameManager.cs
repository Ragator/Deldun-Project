using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool isInputEnabled = true;
    public bool isPlayerInputEnabled = true;

    [SerializeField] private GameObject UICanvas;
    [SerializeField] private Texture2D customCursor;
    [SerializeField] private TextMeshProUGUI currencyCounter;
    [SerializeField] private LevelLoader myLevelLoader;

    public PlayerStats myPlayerStats;

    public Slider healthBar;
    public Slider sanityBar;
    public Slider staminaBar;

    private int currency;

    #region Singleton Pattern
    public static GameManager instance;

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
    #endregion

    private void Start()
    {
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            isPlayerInputEnabled = false;
        }
    }

    public void PlayerDeath()
    {
        myLevelLoader.LoadTargetScene(SceneManager.GetActiveScene().name);
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        myPlayerStats.StartStaminaRegeneration();

        if (scene.name == "Menu")
        {
            isPlayerInputEnabled = false;
        }
        else
        {
            isPlayerInputEnabled = true;
        }
    }
}
