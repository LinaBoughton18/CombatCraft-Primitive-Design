using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEditor;

public class HUDItemSlot : MonoBehaviour
{
    // These HUD item slots basically just need to copy
    // the images & numerical text of whatever the HUD Selection is on right then and there
    
    //=====ITEM SLOT=====//
    // Stores information about the slot itself (display, text, etc.)
    public TMP_Text quantityText; // Displays quantity text
    public Image itemImage; // Picture that displays in the slot
    public Sprite emptySprite;

    public GameObject selectedShader; // Shows if this item is being used or nah
    public bool thisItemSelected; // Determines if the item is currently being used or not

    public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateItemSlot(int itemSlotToCopy)
    {
        // Update image displayed
        // Changes slot's apperance
        itemImage.sprite = inventoryManager.itemSlot[itemSlotToCopy].itemSprite;
        // Update quantity displayed
        // Update quantity text
        quantityText.text = inventoryManager.itemSlot[itemSlotToCopy].quantity.ToString();
        quantityText.enabled = true;
    }
}
