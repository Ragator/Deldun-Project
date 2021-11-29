using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private float playerMaxHealth = 10f;
    [SerializeField] private GameObject UICanvas;

    private float playerHealth;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
}
