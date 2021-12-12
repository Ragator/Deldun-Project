using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField] new private string name = "New Item";
    [SerializeField] private Sprite icon = null;
    [SerializeField] private string description = "This is a new item";

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
}
