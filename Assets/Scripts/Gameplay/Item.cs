using UnityEngine;
using DeldunProject;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] private ItemCategory category;
    [SerializeField] new private string name = "New Item";
    [SerializeField] private Sprite icon = null;
    [SerializeField, TextArea(10, 20)] private string description = "This is a new item";

    public InventorySlot MyInventorySlot { get; set; }

    public string GetName()
    {
        return name;
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetDescription()
    {
        return description;
    }

    public ItemCategory GetCategory()
    {
        return category;
    }

    public virtual void Use()
    {

    }
}
