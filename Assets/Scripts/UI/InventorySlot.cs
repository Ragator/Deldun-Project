using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : UIButton
{
    [SerializeField] private TextMeshProUGUI amountText;

    public Inventory MyInventory { get; set; }
    public Image ItemIcon;

    public Item Item { get; set; }

    protected override void ButtonPressed()
    {
        if (Item.GetCategory() == DeldunProject.ItemCategory.consumable)
        {
            MyInventory.ConsumeItem(Item);
        }
    }

    public virtual void UpdateAmount(int newAmount)
    {
        amountText.text = newAmount.ToString();
    }
}
