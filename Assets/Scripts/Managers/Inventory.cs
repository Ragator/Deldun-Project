using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryUI myInventoryUI;

    private readonly Dictionary<Item, int> items = new Dictionary<Item, int>();

    private readonly List<Item> keys = new List<Item>();

    public void AddItem(Item newItem)
    {
        if (items.ContainsKey(newItem))
        {
            items[newItem]++;
            myInventoryUI.UpdateItemAmount(newItem, items[newItem]);
        }
        else
        {
            items.Add(newItem, 1);
            myInventoryUI.AddNewItem(newItem);
            myInventoryUI.UpdateItemAmount(newItem, items[newItem]);
        }

        if (newItem.GetCategory() == DeldunProject.ItemCategory.key)
        {
            keys.Add(newItem);
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        myInventoryUI.RemoveSlot(itemToRemove);
        items.Remove(itemToRemove);
    }

    public bool CheckKey(Item keyToCheck)
    {
        return keys.Contains(keyToCheck);
    }

    public void ConsumeItem(Item itemUsed)
    {
        items[itemUsed]--;

        if (items[itemUsed] <= 0)
        {
            RemoveItem(itemUsed);
        }
        else
        {
            myInventoryUI.UpdateItemAmount(itemUsed, items[itemUsed]);
        }
    }
}