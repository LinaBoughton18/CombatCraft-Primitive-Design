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
    //public ItemSlot[] hudItemSlots;    We don't need this I think
    public HUDManager HUDManager;
    // An int displaying which HUD row is currently selected (0, 5, 10, 15)
    [SerializeField]
    private int HUDRowSelected;

    /*
    Ok so we can create a list of locations for the HUDSelection to hover over, which would have to correspond with
    the rows of items in the HUD.

    Meanwhile, in the current list of objects we have, we can cycle through them in groups of 5
    So they don't necessairly have to be linked, they just need to appear to be
    
    When 'tab' is pressed (when the menu is activated), we change the location/set of items selected

    OK So we chunk up the initial list by 5's and go from there idk

    InventoryManager sends over the first 5 items in the item slot list to HUDManager
    HUDManager sends the item slot information to the individual item slots
    Inventory Manager tracks the items and communicates with the SpellManager
     
    */

    //======ITEM SLOTS=====//
    // An array of item slots
    public ItemSlot[] itemSlot;
    // An array of the items (in SO form)
    public ItemSO[] itemSOs;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool usable = itemSOs[i].UseItem();
                return usable;
            }
        }
        return false;
    }

    // Adds an item to the inventory
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // Cycles through every item slot in the inventory, starting with the first
        for (int i = 0; i < itemSlot.Length; i++)
        {
            // If the item slot is not full and matches the item already in there
            // OR if the item slot is empty
            if (!itemSlot[i].isFull && itemSlot[i].itemName == itemName || itemSlot[i].quantity <= 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                if (leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);

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
        UpdateHUD();
        return quantity;
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

    public void UpdateHUD()
    {
        // Changes the items displayed in the HUD 
        //HUDManager.UpdateHUD(HUDRowSelected);

        // Changes the location of the HUD Selection bar
        switch (HUDRowSelected)
        {
            case 0:
                HUDSelectionBar.transform.position = new Vector2(0, 306);
                Debug.Log("Error in HUD Bar");
                break;
            case 5:
                HUDSelectionBar.transform.position = new Vector2(0, 101);
                break;
            case 10:
                HUDSelectionBar.transform.position = new Vector2(0, -101);
                break;
            case 15:
                HUDSelectionBar.transform.position = new Vector2(0, -306);
                break;
        }
    }
}