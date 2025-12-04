using UnityEngine;
using System.Collections.Generic;

/*
[System.Serializable]
public class InventoryItem
{
    public string id;
    public string displayName;
    public string textureResourcePath; // location from Assets/Resources/...
}
*/
public class InventoryManager : MonoBehaviour
{
    //public event System.Action<InventoryItem> ItemAddedEvent;
    //public event System.Action<InventoryItem> ItemRemovedEvent;

    //[SerializeField] private List<InventoryItem> items = new List<InventoryItem>();
    //public IReadOnlyList<InventoryItem> Items { get =>  items; }

    /*
    public void AddItem(InventoryItem addedItem)
    {
        if (addedItem == null) return;
        items.Add(addedItem);
        ItemAddedEvent?.Invoke(addedItem);
        Debug.Log($"Added item: {addedItem.displayName}");
    }
    public void RemoveItem(string id)
    {
        if (!HasItem(id)) { Debug.LogWarning($"Inventory has no item with ID {id}"); return; }
        var item = items.Find(x => x.id == id);
        if (item == null) return;
        items.Remove(item);
        ItemRemovedEvent?.Invoke(item);
        Debug.Log($"Removed item: {item.displayName}");
    }
    

    public bool HasItem(string itemID)
    {
        bool has = items.Exists(x => x.id == itemID);
        return has;
    }
    */

}
