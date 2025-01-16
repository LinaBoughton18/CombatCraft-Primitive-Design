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
    public Sprite emptySprite; // An empty sprite that displays a checkmark

    public GameObject selectedShader; // Shows if this item is being used or not

    public InventoryManager inventoryManager;

    // Start is called before the first frame update
    void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void UpdateItemSlot(ItemSlot itemSlotToCopy)
    {
        itemImage.sprite = itemSlotToCopy.itemImage.sprite; // Update image displayed
        quantityText.text = itemSlotToCopy.quantity.ToString(); // Update quantity displayed
        quantityText.enabled = true; // Update quantity text
        selectedShader.SetActive(itemSlotToCopy.isInQueue); // Update selection box in accordance with the itemSlot
    }

}
