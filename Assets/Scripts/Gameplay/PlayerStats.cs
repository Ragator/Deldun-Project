using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    private readonly Dictionary<DamageType, Stat> damageBonuses = new Dictionary<DamageType, Stat>();

    [SerializeField] private Stat maxSanity;
    [SerializeField] private Stat maxStamina;

    private Slider sanityBar;
    private Slider staminaBar;

    [SerializeField] private Stat physicalDamageBonus;
    [SerializeField] private Stat bloodDamageBonus;
    [SerializeField] private Stat arcaneDamageBonus;

    private GameManager myGameManager;

    private int currentSanity;
    private int currentStamina;

    private bool isInvincible = false;

    private bool staminaRegenerationActive = true;

    [SerializeField] private float staminaRegenerationRate = 1f;
    [SerializeField] private float staminaRegenerationCooldown = 0.5f;

    [SerializeField] private float iFramesDuration = 1.5f;
    [SerializeField] private float iFramesDeltaTime = .15f;

    [SerializeField] private GameObject playerSprite;

    private void Awake()
    {
        myGameManager = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<GameManager>();
        myGameManager.myPlayerStats = this;

        healthBar = myGameManager.healthBar;
        sanityBar = myGameManager.sanityBar;
        staminaBar = myGameManager.staminaBar;
    }

    protected override void Start()
    {
        base.Start();

        InitStatBar(maxSanity, ref currentSanity, sanityBar);

        InitStatBar(maxStamina, ref currentStamina, staminaBar);

        damageBonuses.Add(DamageType.Physical, physicalDamageBonus);
        damageBonuses.Add(DamageType.Blood, bloodDamageBonus);
        damageBonuses.Add(DamageType.Arcane, arcaneDamageBonus);
    }

    public override void TakeDamage(int damage, DamageType type)
    {
        if (isInvincible)
        {
            return;
        }

        base.TakeDamage(damage, type);

        StartCoroutine(ActivateIFrames());
    }

    public void ReduceStamina(int staminaCost)
    {
        currentStamina -= staminaCost;
        staminaBar.value = currentStamina;
    }

    private IEnumerator ActivateIFrames()
    {
        isInvincible = true;

        for (float i = 0; i < iFramesDuration; i += iFramesDeltaTime)
        {
            playerSprite.SetActive(!playerSprite.activeSelf);
            yield return new WaitForSeconds(iFramesDeltaTime);
        }

        playerSprite.SetActive(true);
        isInvincible = false;
    }

    protected override void Die()
    {
        base.Die();

        isInvincible = true;

        myGameManager.PlayerDeath();

        currentHealth = maxHealth.Value;
        healthBar.value = currentHealth;
        isInvincible = false;
    }

    public int GetStamina()
    {
        return currentStamina;
    }

    private IEnumerator RegenerateStamina()
    {
        staminaRegenerationActive = true;

        yield return new WaitForSeconds(staminaRegenerationCooldown);

        while (currentStamina < maxStamina.Value)
        {
            currentStamina++;
            staminaBar.value = currentStamina;

            yield return new WaitForSeconds(0.03f / staminaRegenerationRate);
        }

        staminaRegenerationActive = false;
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
}
