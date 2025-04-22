/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

This is a purely UI based class, used to update the HUD bar in game.
It makes no actual changes to the inventory, only updates the appearance of the HUD
when called by InventoryManager.
To update, it cycles through each HUD slot & calls a function for it to update itself.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Should NOT be doing any management of items, inventories, etc.
// Purely visual, getting all of its instructions from InventoryManager

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