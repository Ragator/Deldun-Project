using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    private readonly Dictionary<DamageType, Stat> resistances = new Dictionary<DamageType, Stat>();

    [SerializeField] private AudioClip takeDamageSound;
    [SerializeField] private AudioSource myAudioSource;

    public Stat physicalResistance;
    public Stat bloodResistance;
    public Stat arcaneResistance;

    public Stat maxHealth;
    [SerializeField] protected Slider healthBar;

    public int currentHealth;

    protected virtual void Awake()
    {
        physicalResistance.Value = physicalResistance.GetBaseValue();
        bloodResistance.Value = bloodResistance.GetBaseValue();
        arcaneResistance.Value = arcaneResistance.GetBaseValue();

        resistances.Add(DamageType.Physical, physicalResistance);
        resistances.Add(DamageType.Blood, bloodResistance);
        resistances.Add(DamageType.Arcane, arcaneResistance);
    }

    protected virtual void Start()
    {

    }

    public virtual void TakeDamage(int damage, DamageType type)
    {
        int damageReduction = Mathf.CeilToInt(damage * ((float)resistances[type].Value / 100));

        damage -= damageReduction;

        if (damage < 1)
        {
            damage = 1;
        }

        currentHealth -= damage;

        healthBar.value = currentHealth;

        myAudioSource.PlayOneShot(takeDamageSound);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
    }

    protected void InitStatBar(Stat stat, ref int currentStat, Slider resourceBar)
    {
        stat.Value = stat.GetBaseValue();
        currentStat = stat.Value;
        resourceBar.maxValue = stat.Value;
        resourceBar.value = currentStat;
    }
}

public enum DamageType { Physical, Blood, Arcane }
