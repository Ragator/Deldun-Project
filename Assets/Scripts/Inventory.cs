using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private readonly List<Item> items = new List<Item>();

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }

    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
    }
}
