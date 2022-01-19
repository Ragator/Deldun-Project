using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : UIButton
{
    [SerializeField] Image equipmentIcon;
    [SerializeField] InventoryUI myInventoryUI;

    public Equipment EquippedItem { get; set; }
    public Image EquipmentIcon { get; set; }
    public bool HasEquipment { get; set; }

    private Image defaultIcon;

    protected override void Start()
    {
        base.Start();
        defaultIcon = GetComponent<Image>();
    }

    protected override void ButtonPressed()
    {
        if (HasEquipment)
        {
            EquippedItem.Use();
        }
    }

    public void MouseEnter()
    {
        if (EquippedItem != null)
        {
            myInventoryUI.UpdateTextBoxes(EquippedItem);
        }
    }

    public void EquipItem(Equipment itemToEquip)
    {
        EquippedItem = itemToEquip;
        EquippedItem.isEquipped = true;
        HasEquipment = true;

        equipmentIcon.sprite = itemToEquip.GetIcon();
        equipmentIcon.enabled = true;

        defaultIcon.enabled = false;
    }

    public void UnequipItem()
    {
        HasEquipment = false;

        equipmentIcon.sprite = null;
        equipmentIcon.enabled = false;

        defaultIcon.enabled = true;

        EquippedItem = null;
    }
}
