using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEditor;

public class ItemSlot : MonoBehaviour, IPointerClickHandler //adds clickability
{
    //=====ITEM DATA=====//
    // Stores the information about the item
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull; // tracks if this slot is occupied or not
    public string itemDescription;
    public Sprite emptySprite; // An empty sprite that shows a checkmark

    //=====ITEM SLOT=====//
    // Stores information about the slot itself (display, text, etc.)
    [SerializeField]
    private TMP_Text quantityText; // Displays quantity text

    [SerializeField]
    public Image itemImage; // Picture that displays in the slot

    public GameObject selectedShader;
    public bool thisItemSelected;

    public bool isInQueue; // Notates if an item is in the spellQueue or not

    private int maxNumberOfItems = 999; // The maximum number of item that can fit in an item slot

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
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        // Check to see if the slot is already full, if it is then return the quantity
        if (isFull)
        {
            return quantity;
        }

        // Updates the item data
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        
        // Changes slot's apperance
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        // Updates the text to show that the slot is full
        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;
        
            // Return the LEFTOVERS
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }

        // Update quantity text
        quantityText.text = this.quantity.ToString();
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
        // NOT NEEDED FOR ME (PART OF TUTORIAL) BUT STILL HANDY TO REFERENCE
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
            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;
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

    public void OnRightClick()
    {
        // Create a new item that spawns by the player
        // when an inventory item is dropped
        // Creates a gameobject with the name itemName
        GameObject itemToDrop = new GameObject(itemName);
        // Puts the Item script on our game object
        Item newItem = itemToDrop.AddComponent<Item>();
        
        // Sets the data of the item
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        // Create and modify the sprite renderer
        // Adding the sprite renderer to the newly created item
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
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
        this.quantity -= 1;
        quantityText.text = this.quantity.ToString();

        // If quantity = 0, empty the slot
        if (this.quantity <= 0)
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
