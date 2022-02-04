using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    private GameManager myGameManager;
    [SerializeField] private Player myPlayer;
    private Slider sanityBar;
    private Slider staminaBar;

    public delegate void OnStatModified();
    public OnStatModified onStatModifiedCallback;

    private readonly Dictionary<DamageType, Stat> damageBonuses = new Dictionary<DamageType, Stat>();

    public Stat maxSanity;
    public Stat maxStamina;

    [SerializeField] private Stat physicalDamageBonus;
    [SerializeField] private Stat bloodDamageBonus;
    [SerializeField] private Stat arcaneDamageBonus;

    public int currentSanity;
    public int currentStamina;

    [SerializeField] private float staminaRegenerationRate = 1f;
    [SerializeField] private float staminaRegenerationCooldown = 0.5f;

    private readonly Dictionary<EquipmentModifier, Stat> modifiableStats = new Dictionary<EquipmentModifier, Stat>();

    public int playerLevel;

    public long levelUpCost;

    [Header ("Player Levels")]
    public Stat longevity;
    public Stat fitness;
    public Stat willpower;
    public Stat brawn;
    public Stat skill;
    public Stat vision;

    protected override void Awake()
    {
        base.Awake();
        
        myGameManager = GameObject.FindWithTag(DeldunProject.Tags.gameManager).GetComponent<GameManager>();
        myGameManager.myPlayerStats = this;

        healthBar = myGameManager.healthBar;
        sanityBar = myGameManager.sanityBar;
        staminaBar = myGameManager.staminaBar;

        InitStatBar(maxHealth, ref currentHealth, healthBar);
        InitStatBar(maxSanity, ref currentSanity, sanityBar);
        InitStatBar(maxStamina, ref currentStamina, staminaBar);

        physicalDamageBonus.Value = physicalDamageBonus.GetBaseValue();
        bloodDamageBonus.Value = bloodDamageBonus.GetBaseValue();
        arcaneDamageBonus.Value = arcaneDamageBonus.GetBaseValue();

        longevity.Value = longevity.GetBaseValue();
        fitness.Value = fitness.GetBaseValue();
        willpower.Value = willpower.GetBaseValue();
        brawn.Value = brawn.GetBaseValue();
        skill.Value = skill.GetBaseValue();
        vision.Value = vision.GetBaseValue();

        damageBonuses.Add(DamageType.Physical, physicalDamageBonus);
        damageBonuses.Add(DamageType.Blood, bloodDamageBonus);
        damageBonuses.Add(DamageType.Arcane, arcaneDamageBonus);

        modifiableStats.Add(EquipmentModifier.physicalResistance, physicalResistance);
        modifiableStats.Add(EquipmentModifier.bloodResistance, bloodResistance);
        modifiableStats.Add(EquipmentModifier.arcaneResistance, arcaneResistance);
        modifiableStats.Add(EquipmentModifier.physicalDamageBonus, physicalDamageBonus);
        modifiableStats.Add(EquipmentModifier.bloodDamageBonus, bloodDamageBonus);
        modifiableStats.Add(EquipmentModifier.arcaneDamageBonus, arcaneDamageBonus);
        modifiableStats.Add(EquipmentModifier.maxHealth, maxHealth);
        modifiableStats.Add(EquipmentModifier.maxSanity, maxSanity);
        modifiableStats.Add(EquipmentModifier.maxStamina, maxStamina);

        modifiableStats.Add(EquipmentModifier.longevity, longevity);
        modifiableStats.Add(EquipmentModifier.fitness, fitness);
        modifiableStats.Add(EquipmentModifier.willpower, willpower);
        modifiableStats.Add(EquipmentModifier.brawn, brawn);
        modifiableStats.Add(EquipmentModifier.skill, skill);
        modifiableStats.Add(EquipmentModifier.vision, vision);
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void TakeDamage(int damage, DamageType type)
    {
        if (myPlayer.isInvincible)
        {
            return;
        }

        base.TakeDamage(damage, type);

        if (!myPlayer.isInvincible)
        {
            myPlayer.BecomeInvincible();
        }
    }

    protected override void Die()
    {
        base.Die();

        myPlayer.isInvincible = true;

        myGameManager.PlayerDeath();

        currentHealth = maxHealth.Value;
        healthBar.value = currentHealth;
    }

    public int GetStamina()
    {
        return currentStamina;
    }

    public void ReduceStamina(int staminaCost)
    {
        currentStamina -= staminaCost;
        staminaBar.value = currentStamina;
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(staminaRegenerationCooldown);

        while (currentStamina < maxStamina.Value)
        {
            currentStamina++;
            staminaBar.value = currentStamina;

            yield return new WaitForSeconds(0.03f / staminaRegenerationRate);
        }
    }

    public void StopStaminaRegeneration()
    {
        StopCoroutine("RegenerateStamina");
    }

    public void StartStaminaRegeneration()
    {
        StopCoroutine("RegenerateStamina");
        StartCoroutine("RegenerateStamina");
    }

    public void LevelUp(EquipmentModifier statToLevelUp)
    {
        modifiableStats[statToLevelUp].SetBaseValue(modifiableStats[statToLevelUp].GetBaseValue() + 1);
    }

    public void AddEquipment(Equipment equipmentToAdd)
    {
        bool statGotModified = false;
        foreach (StatModifier modifier in equipmentToAdd.modifiers)
        {
            modifiableStats[modifier.modifierName].AddModifier(modifier.modifierValue, modifier.modifierType);
            HandleStatModification(modifier.modifierName);
            statGotModified = true;
        }

        if (statGotModified)
        {
            onStatModifiedCallback.Invoke();
        }
    }

    public void RemoveEquipment(Equipment equipmentToRemove)
    {
        bool statGotModified = false;
        foreach (StatModifier modifier in equipmentToRemove.modifiers)
        {
            modifiableStats[modifier.modifierName].RemoveModifier(modifier.modifierValue, modifier.modifierType);         
            HandleStatModification(modifier.modifierName);
            statGotModified = true;
        }

        if (statGotModified)
        {
            onStatModifiedCallback.Invoke();
        }
    }

    private void HandleStatModification(EquipmentModifier statModified)
    {
        switch (statModified)
        {
            case EquipmentModifier.maxHealth:
                UpdateStatBarMax(maxHealth, currentHealth, healthBar);
                break;
            case EquipmentModifier.maxSanity:
                UpdateStatBarMax(maxSanity, currentSanity, sanityBar);
                break;
            case EquipmentModifier.maxStamina:
                UpdateStatBarMax(maxStamina, currentStamina, staminaBar);
                break;
        }
    }

    private void UpdateStatBarMax(Stat stat, int currentStat, Slider resourceBar)
    {
        if (currentStat > stat.Value)
        {
            currentStat = stat.Value;
            resourceBar.value = currentStat;
        }

        resourceBar.maxValue = stat.Value;
    }
}