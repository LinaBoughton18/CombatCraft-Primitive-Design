using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The HUD should NOT be doing any management of items, inventories, etc.
// The HUD is purely visual, getting all of its instructions from InventoryManager, SpellController, etc.

public class HUDManager : MonoBehaviour
{
    private InventoryManager inventoryManager;
    // An array of the 5 HUD item slots
    public HUDItemSlot[] HUDitemSlot;

    void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Updates the HUD visually
    // Pulls information from the InventoryManager to update the images, selectedShaders, and quantity texts
    // This should NOT be editing any data in InventoryManager or ItemSlot, just updating itself with the same information
    public void UpdateHUD(int HUDRowActive, ItemSlot[] itemSlot)
    {
        // Iterates through the HUDItemSlots, editing the picture, selection box, etc. to match the corresponding inventory row
        // Does this by prompting each HUDItemSlot to edit itself, passing the corresponding inventory slot as reference
        for (int i = 0; i < HUDitemSlot.Length; i++)
        {
            int itemSlotToCopy = HUDRowActive + i;
            HUDitemSlot[i].UpdateItemSlot(itemSlot[itemSlotToCopy]);
        }
    }
}
