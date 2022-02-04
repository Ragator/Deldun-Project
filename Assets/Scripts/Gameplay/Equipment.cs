using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    [HideInInspector] public bool isEquipped;
    public EquipmentType equipType;

    public Sprite equippedSprite;
    public GameObject weapon;

    public List<StatModifier> modifiers;

    private void OnEnable()
    {
        isEquipped = false;
    }

    public override void Use()
    {
        if (isEquipped)
        {
            Unequip();
        }
        else
        {
            Equip();
        }
    }

    private void Equip()
    {
        MyInventorySlot.EquipItem();
        isEquipped = true;
    }

    private void Unequip()
    {
        MyInventorySlot.UnequipItem();
        isEquipped = false;
    }
}

public enum EquipmentType { Weapon, Head, Chest, Accessory }

public enum EquipmentModifier { 
    physicalResistance, 
    bloodResistance, 
    arcaneResistance, 
    physicalDamageBonus, 
    bloodDamageBonus, 
    arcaneDamageBonus, 
    maxHealth,
    maxSanity,
    maxStamina,
    longevity,
    fitness,
    willpower,
    brawn,
    skill,
    vision
}

[Serializable]
public class StatModifier
{
    public EquipmentModifier modifierName;
    public int modifierValue;
    public ModifierType modifierType;
}