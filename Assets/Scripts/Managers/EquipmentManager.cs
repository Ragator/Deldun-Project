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

    private EquipmentSlot slotToEquipIn;

    private void Start()
    {
        currentEquipment[0] = weaponSlot;
        currentEquipment[1] = headSlot;
        currentEquipment[2] = chestSlot;
        currentEquipment[3] = accessorySlot1;
        currentEquipment[4] = accessorySlot2;
        currentEquipment[5] = accessorySlot3;
    }

    public EquipmentSlot Equip(Equipment newItem)
    {
        if (newItem.equipType == EquipmentType.Accessory)
        {
            slotToEquipIn = FindEmptySlot();
        }
        else
        {
            slotToEquipIn = currentEquipment[(int)newItem.equipType];
            
        }

        EquipInSlot(slotToEquipIn, newItem);
        return slotToEquipIn;
    }

    private EquipmentSlot FindEmptySlot()
    {
        if (accessorySlot1.HasEquipment)
        {
            if (accessorySlot2.HasEquipment)
            {
                if (accessorySlot3.HasEquipment)
                {
                    accessorySlot1.EquippedItem.Use();
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
