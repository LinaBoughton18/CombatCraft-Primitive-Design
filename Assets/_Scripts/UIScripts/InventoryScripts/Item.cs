using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script can be applied to every physical item in the game :)
public class Item : MonoBehaviour
{
    //====== DATA =====//
    // The SO that this item pulls its data from
    public ItemSO itemSO;

    // Number of items collected when picking up the item
    // For now, it's set to 1 at the start, since it's just one item
    public int quantity = 1;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When colliding with the player
        if (collision.gameObject.tag == "Player")
        {
            // sends a message to the manager about how many items are left over in the stack
            int leftOverItems = inventoryManager.AddItem(itemSO, quantity);

            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            // If there are leftovers, change the quantity of the item
            else
            {
                quantity = leftOverItems;
            }
        }
    }
}