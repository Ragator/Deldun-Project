using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public bool isEquipped = false;
    public EquipmentType equipType;

    public override void Use()
    {
        if (isEquipped)
        {
            Debug.Log("Unequipped");
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