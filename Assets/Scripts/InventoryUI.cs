using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory myInventory;
    [SerializeField] private GameObject defaultInventorySlot;
    [SerializeField] private GameObject consumablesParent;

    public void addNewItem(Item itemToAdd)
    {
        InventorySlot myNewItem = Instantiate(defaultInventorySlot).GetComponent<InventorySlot>();
        myNewItem.Item = itemToAdd;
        myNewItem.ItemIcon.sprite = itemToAdd.GetIcon();
        myNewItem.transform.parent = consumablesParent.transform;
        myNewItem.transform.localScale = new Vector3(1, 1, 1);
    }
}
