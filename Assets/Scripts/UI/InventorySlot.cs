using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : UIButton
{
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image equippedIcon;

    public Inventory MyInventory { get; set; }
    public InventoryUI MyInventoryUI { get; set; }
    public EquipmentManager MyEquipmentManager { get; set; }
    public Image ItemIcon;

    public Item Item { get; set; }

    private EquipmentSlot equippedSlot;

    protected override void Start()
    {
        base.Start();
        Item.MyInventorySlot = this;
    }

    protected override void ButtonPressed()
    {
        if (Item.GetCategory() == DeldunProject.ItemCategory.consumable)
        {
            MyInventory.ConsumeItem(Item);
        }
        else if (Item is Equipment)
        {
            Item.Use();
        }
    }

    public virtual void UpdateAmount(int newAmount)
    {
        amountText.text = newAmount.ToString();
    }

    public void MouseEnter()
    {
        MyInventoryUI.UpdateTextBoxes(Item);
    }

    public void EquipItem()
    {
        equippedIcon.enabled = true;
        equippedSlot = MyEquipmentManager.Equip((Equipment)Item);
    }

    public void UnequipItem()
    {
        equippedIcon.enabled = false;
        equippedSlot.UnequipItem();
    }
}
