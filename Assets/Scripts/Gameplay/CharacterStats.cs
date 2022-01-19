using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    private readonly Dictionary<DamageType, Stat> resistances = new Dictionary<DamageType, Stat>();

    [SerializeField] private AudioClip takeDamageSound;
    [SerializeField] private AudioSource myAudioSource;

    [SerializeField] private Stat physicalResistance;
    [SerializeField] private Stat bloodResistance;
    [SerializeField] private Stat arcaneResistance;

    [SerializeField] protected Stat maxHealth;
    [SerializeField] protected Slider healthBar;

    protected int currentHealth;

    protected virtual void Start()
    {
        InitStatBar(maxHealth, ref currentHealth, healthBar);

        resistances.Add(DamageType.Physical, physicalResistance);
        resistances.Add(DamageType.Blood, bloodResistance);
        resistances.Add(DamageType.Arcane, arcaneResistance);
    }

    public virtual void TakeDamage(int damage, DamageType type)
    {
        int damageReduction = Mathf.CeilToInt(damage * (resistances[type].Value / 100));
        damage -= damageReduction;

        if (damage < 1)
        {
            damage = 1;
        }

        currentHealth -= damage;

        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }

        myAudioSource.PlayOneShot(takeDamageSound);
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
