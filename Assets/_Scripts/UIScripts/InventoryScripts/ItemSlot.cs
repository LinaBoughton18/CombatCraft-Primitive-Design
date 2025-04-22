/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

Manages an individual item slot in the player's inventory (every slot has this script attached).

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEditor;

public class ItemSlot : MonoBehaviour, IPointerClickHandler // adds clickability
{
    //===== ITEM DATA =====//
    public ItemSO itemSO; // A reference to the data about the item

    //====== ITEM SLOT DATA =====//
    public int slotQuantity; // tracks the number of items in the slot
    public bool isFull; // tracks if this slot is occupied or not
    public Sprite emptySprite; // An empty sprite that shows a checkmark
    private int maxNumberOfItems = 999; // The maximum number of item that can fit in an item slot

    public bool isInQueue; // Notates if an item is in the spellQueue or not

    //===== SLOT VISUALS =====//
    // Stores information about the visual slot (display, text, etc.)
    [SerializeField]
    private TMP_Text quantityText; // Displays quantity text
    [SerializeField]
    public Image itemImage; // Picture that displays in the slot

    public GameObject selectedShader;
    public bool thisItemSelected;

    //====ITEM DESCRIPTION SLOT====//
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    //====MISC====//
    private InventoryManager inventoryManager;
    
    void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        isInQueue = false;
        isFull = false;
    }

    // Called by inventory manager to try to add an item to this slot
    public int AddItem(ItemSO itemSO, int quantity)
    {
        // Check to see if the slot is already full, if it is then return the quantity & exit
        if (isFull)
        {
            return quantity;
        }

        this.itemSO = itemSO;

        // Changes slot's apperance
        itemImage.sprite = itemSO.sprite;

        // Updates the text to show that the slot is full
        slotQuantity += quantity;
        if (slotQuantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;
        
            // Return the LEFTOVERS
            int extraItems = slotQuantity - maxNumberOfItems;
            slotQuantity = maxNumberOfItems;
            return extraItems;
        }

        // Update quantity text
        quantityText.text = slotQuantity.ToString();
        quantityText.enabled = true;

        return 0;
    }

    // Detects if this object has been clicked via left or right
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        // This bit is CURRENTLY UNUSED (part of the tutorial I referenced, but still handy to reference)
        // In the inventory, when you click on an already selected itemSlot, use the item
        /*
        // If the slot is already selected, use the item
        if (thisItemSelected)
        {
            bool usable = inventoryManager.UseItem(itemName);
            if (usable)
            {
                SubtractItem();
            }
        }
        else
        {
        */
        // Selectes the itemSlot in the inventory, displaying it on the righthand side
            // Deselects all other inventory spots and selects this one
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;

            // Edits the UI for the description section
            itemDescriptionNameText.text = itemSO.itemName;
            itemDescriptionText.text = itemSO.itemDescription;
            itemDescriptionImage.sprite = itemSO.sprite;
            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        //}
    }

    // Removes data from the slot
    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }

    // When the item slot is right-clicked, drop the item
    public void OnRightClick()
    {
        // Create a new item that spawns by the player
        // when an inventory item is dropped
        // Creates a gameobject with the name itemName
        GameObject itemToDrop = new GameObject(itemSO.itemName);
        // Puts the Item script on our game object

        // Adds the proper itemSO to our item
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.itemSO = itemSO; // ERROR = this sets it so that all but 1 of the items are lost, dropping 1 singular item

        // Create and modify the sprite renderer
        // Adding the sprite renderer to the newly created item
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSO.sprite;
        // Sorting order determines the layer in the hierarchy
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Ground";

        // Add a collider to the item
        itemToDrop.AddComponent<BoxCollider2D>();

        // Set the location of the item
        // If you have multiple objects that function as a player
        // (i.e. multiplayer) you can use FindWithTag("Player) instead!
        itemToDrop.transform.position = GameObject.Find("Player").transform.position + new Vector3(1,0,0);
        // Downsize the items by .5, making them smaller
        itemToDrop.transform.localScale = new Vector3(.5f, .5f, .5f);

        // Subtracts the item from the inventory
        SubtractItem();
    }

    public void SubtractItem()
    {
        // Subtracts the item from the inventory & updates display text
        slotQuantity -= 1;
        quantityText.text = slotQuantity.ToString();

        // If quantity = 0, empty the slot
        if (slotQuantity <= 0)
        {
            EmptySlot();
        }
    }

    // The itemSlot now knows that it is in the queue (changes it's selected bar)
    public void MakeQueued()
    {
        isInQueue = true;
        selectedShader.SetActive(isInQueue);
    }

    // The itemSlot now knows that it is not in the queue (changes it's selected bar)
    public void MakeDequeued()
    {
        isInQueue = false;
        selectedShader.SetActive(isInQueue);
    }
}
