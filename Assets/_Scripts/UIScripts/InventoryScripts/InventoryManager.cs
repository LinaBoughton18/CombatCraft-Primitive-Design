/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

Manages the player's inventory, stored as an array, itemSlot[].
Handles adding & removing items & adding/removing stuff from the spell queue.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InventoryManager : MonoBehaviour
{
    //=====INVENTORY MENU=====//
    public GameObject inventoryMenu;
    private bool menuActivated;

    //=====HUD SELECTION=====//
    public GameObject HUDSelectionBar;
    //public ItemSlot[] hudItemSlots;    (We don't need this methinks)
    public HUDManager HUDManager;
    // An int displaying which HUD row is currently selected (row1, row2, row3, row4) = (0, 5, 10, 15)
    public int HUDRowSelected;

    //======ITEM SLOTS=====//
    // An array of item slots, which form the inventory
    public ItemSlot[] itemSlot;
    // An array of the items (in SO form)
    public ItemSO[] itemSOs;
    
    // Start is called before the first frame update
    void Start()
    {
        HUDManager = GameObject.Find("HUDCanvas").GetComponent<HUDManager>();

        UpdateHUD();
    }

    // Responds to player input
    void Update()
    {
        // Getting User Input

        // Opens inventory and closes it
        if (Input.GetButtonDown("Inventory") && menuActivated)
        {
            //Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            menuActivated = false;
        }
        else if (Input.GetButtonDown("Inventory") && !menuActivated)
        {
            // This line pauses time, but I don't really want that
            //Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            menuActivated = true;
        }
        // Changes HUDRowSelection to the next row
        else if (Input.GetButtonDown("HUDRowSelection"))
        {
            // Alters the row in the inventory that the HUDSelectionBar is on
            HUDRowSelected += 5;
            if (HUDRowSelected > 15)
            {
                HUDRowSelected = 0;
            }

            UpdateHUD();
        }
    }

    // Called when we use the item
    // CURRENTLY UNUSED BECAUSE THE TUTORIAL I REFERENCED USES ITEMS DIFFERENTLY THAN I DO
    // but I wanted to keep it around for reference
    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                //bool usable = itemSOs[i].UseItem();

                UpdateHUD();
                
                //return usable;
            }
        }
        return false;
    }

    // Adds an item to the inventory
    public int AddItem(ItemSO itemSO, int quantity)
    {
        // Cycles through every item slot in the inventory, starting with the first
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull && itemSlot[i].itemSO == itemSO || itemSlot[i].slotQuantity <= 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemSO, quantity);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemSO, leftOverItems);
                    // THIS LINE ABOVE USED TO TAKE THE LEFTOVERS AND ADD THEM TO A DIFFERENT SLOT, I GOT RID OF IT, MAY NEED TO BE FIXED IDK

                    UpdateHUD();
                    return leftOverItems;
                }
                else
                {
                    UpdateHUD();
                    return 0;
                }
            }
        }
        // If there are no avaiable slots in the inventory:
        UpdateHUD();
        return quantity;
    }

    // i refers to the 1-5 key pressed
    public ItemSlot QueueItem(int correspondingHUDSlot)
    {
        int inventoryIndexToCheck = HUDRowSelected + correspondingHUDSlot;
        // If item slot is empty -> return nothing
        if (itemSlot[inventoryIndexToCheck].slotQuantity <= 0)
        {
            return null;
        }
        // If the item slot is not empy ->
        // Updates the itemSlot so that it knows that it's queued, then updates the visuality of the HUD
        // EDIT: I should make it so that it'll also update the inventory, but I can do that later)
        // Returns a reference to the inventory slot
        else
        {
            itemSlot[inventoryIndexToCheck].MakeQueued();
            UpdateHUD();

            return itemSlot[inventoryIndexToCheck];
        }
    }

    // Removes an item from the spellqueue, the spell has not been cast
    // Replaces it with null & updating the visuals with the HUD
    public ItemSlot DequeueItem(int HUDIndex)
    {
        itemSlot[HUDRowSelected + HUDIndex].MakeDequeued();
        UpdateHUD();

        return null;
    }

    public void DecrementItemsInSpell(ItemSlot[] spellQueue)
    {
        // Decremenets the item quantities for each item in the SpellQueue
        for (int i = 0; i < spellQueue.Length; i++)
        {
            if (spellQueue[i] != null)
            {
                // Calls the itemSlot and has it subtract the item's quantity
                spellQueue[i].SubtractItem();
                spellQueue[i].MakeDequeued();
            }
        }

        UpdateHUD();
    }

    // Deselects all slots in the inventory
    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    // Handles updates the the HUD
    public void UpdateHUD()
    {
        // Changes the items displayed in the HUD 
        HUDManager.UpdateHUD(HUDRowSelected, itemSlot);

        // Changes the visual location of the HUD Selection bar in the inventory
        RectTransform HUDTransform = HUDSelectionBar.GetComponent<RectTransform>();
        switch (HUDRowSelected)
        {
            case 0: // First Row Selected
                HUDTransform.anchoredPosition = new Vector2(0, 306);
                break;
            case 5:
                HUDTransform.anchoredPosition = new Vector2(0, 101);
                break;
            case 10:
                HUDTransform.anchoredPosition = new Vector2(0, -101);
                break;
            case 15:
                HUDTransform.anchoredPosition = new Vector2(0, -306);
                break;
        }
    }
}