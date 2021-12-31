using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory myInventory;
    [SerializeField] private GameObject defaultInventorySlot;
    [SerializeField] private GameObject consumablesParent;

    private readonly Dictionary<Item, InventorySlot> slots = new Dictionary<Item, InventorySlot>();

    public void AddNewItem(Item itemToAdd)
    {
        InventorySlot newSlot = Instantiate(defaultInventorySlot).GetComponent<InventorySlot>();
        newSlot.MyInventory = myInventory;
        newSlot.Item = itemToAdd;
        newSlot.ItemIcon.sprite = itemToAdd.GetIcon();
        newSlot.transform.parent = consumablesParent.transform;
        newSlot.transform.localScale = new Vector3(1, 1, 1);

        slots.Add(itemToAdd, newSlot);
    }

    public void UpdateItemAmount(Item itemToIncrease, int newAmount)
    {
        slots[itemToIncrease].UpdateAmount(newAmount);
    }

    public void RemoveSlot(Item itemToRemove)
    {
        Destroy(slots[itemToRemove].gameObject);
        slots.Remove(itemToRemove);
    }
}
