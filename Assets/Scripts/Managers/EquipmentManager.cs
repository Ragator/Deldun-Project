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

    readonly EquipmentSlot[] currentEquipment = new EquipmentSlot[6];

    private EquipmentSlot slotToEquipIn;

    private PlayerStats myPlayerStats;
    private Player myPlayer;

    private GameObject equippedWeapon;

    private void Start()
    {
        currentEquipment[0] = weaponSlot;
        currentEquipment[1] = headSlot;
        currentEquipment[2] = chestSlot;
        currentEquipment[3] = accessorySlot1;
        currentEquipment[4] = accessorySlot2;
        currentEquipment[5] = accessorySlot3;

        myPlayerStats = GameObject.FindWithTag(DeldunProject.Tags.player).GetComponent<PlayerStats>();
        myPlayer = myPlayerStats.gameObject.GetComponent<Player>();
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
        if (slot.HasEquipment)
        {
            slot.EquippedItem.Use();
        }

        slot.EquipItem(item);
        myPlayerStats.AddEquipment(item);

        if (slot == headSlot)
        {
            myPlayer.helmetSlot.sprite = item.equippedSprite;
            myPlayer.helmetSlot.enabled = true;
        }
        else if (slot == chestSlot)
        {
            myPlayer.chestpieceSlot.sprite = item.equippedSprite;
            myPlayer.helmetSlot.enabled = true;
        }
        else if (slot == weaponSlot)
        {
            equippedWeapon = Instantiate(item.weapon);
            equippedWeapon.transform.parent = myPlayer.weaponSlot;
            equippedWeapon.transform.localPosition = Vector2.zero;
        }
    }

    public void UnequipItem(EquipmentSlot slot, Equipment item)
    {
        myPlayerStats.RemoveEquipment(item);

        if (slot == headSlot)
        {
            myPlayer.helmetSlot.enabled = false;
        }
        else if (slot == chestSlot)
        {
            myPlayer.helmetSlot.enabled = false;
        }
        else if (slot == weaponSlot)
        {
            Destroy(equippedWeapon);
            equippedWeapon = null;
        }
    }
}
