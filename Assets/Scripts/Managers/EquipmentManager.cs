using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] EquipmentSlot weaponSlot;
    [SerializeField] EquipmentSlot headSlot;
    [SerializeField] EquipmentSlot chestSlot;
    [SerializeField] EquipmentSlot accessorySlot1;
    [SerializeField] EquipmentSlot accessorySlot2;
    [SerializeField] EquipmentSlot accessorySlot3;

    EquipmentSlot[] currentEquipment = new EquipmentSlot[6];

    private void Start()
    {
        currentEquipment[0] = weaponSlot;
        currentEquipment[1] = headSlot;
        currentEquipment[2] = chestSlot;
        currentEquipment[3] = accessorySlot1;
        currentEquipment[4] = accessorySlot2;
        currentEquipment[5] = accessorySlot3;
    }

    public void Equip(Equipment newItem)
    {
        if (newItem.equipType == EquipmentType.Accessory)
        {
            EquipInSlot(FindEmptySlot(), newItem);
        }
        else
        {
            EquipInSlot(currentEquipment[(int)newItem.equipType], newItem);
        }
    }

    private EquipmentSlot FindEmptySlot()
    {
        if (accessorySlot1.HasEquipment)
        {
            if (accessorySlot2.HasEquipment)
            {
                if (accessorySlot3.HasEquipment)
                {
                    return accessorySlot1;
                }
                else
                {
                    return accessorySlot3;
                }
            }
            else
            {
                return accessorySlot2;
            }
        }
        else
        {
            return accessorySlot1;
        }
    }

    private void EquipInSlot(EquipmentSlot slot, Equipment item)
    {
        slot.EquipItem(item);
    }
}
