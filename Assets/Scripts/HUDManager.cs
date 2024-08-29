using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    // OK, so we want to send the first 5 items in the inventory into the hotbar

    //=====User Experiences=====//
    // Hotbar displays 5 items, along with their quantity
    // The hotbar items automatically correspond to a row in the inventory
    // Items can only be used from the hotbar, via holding the mouse down and pressing 1-5
    // Pressing 'Tab' lets the player cycle through the rows in the inventory into their hotbar (like in Stardew)
    // Items in the inventory can be rearranged by dragging to different spots
    // Items cannot be used by clicking on them in the inventory

    private InventoryManager inventoryManager;
    // An array of the 5 item slots
    public HUDItemSlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //inventoryManager.itemSlot[1].quantity++;
    }

    // Updates the HUD with the new images
    public void UpdateHUD(int HUDRowActive) 
    {
        DeselectAllSlots();

        for (int i = 0; i < itemSlot.Length; i++)
        {
            // Update the HUDitemSlot at i with the corresponding inventory hud row
            itemSlot[i].UpdateItemSlot(HUDRowActive + i);
        }
    }

    // Deselects all slots in the HUD
    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
