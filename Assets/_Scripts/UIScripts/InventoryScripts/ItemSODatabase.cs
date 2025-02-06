using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSODatabase : MonoBehaviour
{
    // A list of all the itemSOs
    public List<ItemSO> ItemSOList; // Manually add the ItemSOs to the list in Unity
    // A dictionary of all the itemSOs
    public Dictionary<string, ItemSO> ItemSODictionary;

    private void Awake()
    {
        CreateItemSODictionary();
    }

    private void CreateItemSODictionary()
    {
        // Creates a dictionary
        ItemSODictionary = new Dictionary<string, ItemSO>();

        // Runs through the list of itemSOs,
        // creating a new dictionary entry for each
        // itemName is used as the key (not name, but itemName!)
        foreach (ItemSO item in ItemSOList)
        {
            ItemSODictionary[item.itemName] = item;
        }
    }

    // A retrieval method for grabbing this item from somewhere else
    public ItemSO GetItemSOByName(string itemName)
    {
        if (ItemSODictionary.TryGetValue(itemName, out var obj))
        {
            return obj;
        }
        // Just in case there's a missing entry for whatever
        Debug.Log("ItemSO location for " + itemName + " is null");
        return null;
    }
}