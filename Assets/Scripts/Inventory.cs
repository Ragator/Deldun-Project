using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] InventoryUI myInventoryUI;

    private readonly List<Item> items = new List<Item>();

    private readonly List<Item> keys = new List<Item>();

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
        myInventoryUI.addNewItem(newItem);

        if (newItem.GetCategory() == DeldunProject.ItemCategory.key)
        {
            keys.Add(newItem);
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
    }

    public bool CheckKey(Item keyToCheck)
    {
        return keys.Contains(keyToCheck);
    }
}
