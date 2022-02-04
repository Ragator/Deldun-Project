using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;

    private readonly List<int> additiveModifiers = new List<int>();
    private readonly List<int> multiplicativeModifiers = new List<int>();

    public int Value { get; set; }

    public void SetBaseValue(int newValue)
    {
        baseValue = newValue;
        CalculateValue();
    }

    public int GetBaseValue()
    {
        return baseValue;
    }

    public void CalculateValue()
    {
        Value = baseValue;
        additiveModifiers.ForEach(x => Value += x);
        int tempValue = Value;
        multiplicativeModifiers.ForEach(x => Value += Mathf.CeilToInt(tempValue * ((float)x / 100)));
    }

    public void AddModifier(int modifier, ModifierType type)
    {
        if (type == ModifierType.Additive)
        {
            additiveModifiers.Add(modifier);
        }
        else
        {
            multiplicativeModifiers.Add(modifier);
        }

        CalculateValue();
    }

    public void RemoveModifier(int modifier, ModifierType type)
    {
        if (type == ModifierType.Additive)
        {
            additiveModifiers.Remove(modifier);
        }
        else
        {
            multiplicativeModifiers.Remove(modifier);
        }

        CalculateValue();
    }
}

public enum ModifierType { Additive, Multiplicative }