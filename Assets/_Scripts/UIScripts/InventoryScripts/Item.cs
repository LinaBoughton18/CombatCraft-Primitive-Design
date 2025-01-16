using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // This script can be applied to every item in the game :)

    //====ITEM DATA====//
    [SerializeField] //A tag that makes the variable
    // visible & editable in the Unity Inspector
    public string itemName;

    // Number of items collected when picking up the item
    [SerializeField]
    public int quantity;

    // Keeps track of the image of the item
    [SerializeField]
    public Sprite sprite;

    [TextArea] // This line gives us a big box in the Unity Editor
    // in which we can edit the text!
    [SerializeField]
    public string itemDescription;

    //====MISC====//
    private InventoryManager inventoryManager;
    
    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When colliding with the player
        if (collision.gameObject.tag == "Player")
        {
            // sends a message to the manager about how many items are left over in the stack
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription);

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