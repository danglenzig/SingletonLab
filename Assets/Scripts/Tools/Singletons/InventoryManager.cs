using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InventoryItem
{
    public string id;
    public string displayName;
    public string textureResourcePath; // location from Assets/Resources/...
}

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();
    public IReadOnlyList<InventoryItem> Items { get =>  items; }

    public void AddItem(InventoryItem addedItem)
    {
        if (addedItem == null) return;

    }
}
