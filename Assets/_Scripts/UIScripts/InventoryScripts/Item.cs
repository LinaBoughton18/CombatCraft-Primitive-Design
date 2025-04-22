/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

Handles common behavior for items, while the script ItemSO handles all unique information.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

// This script can be applied to every physical item in the game :)
public class Item : MonoBehaviour
{
    #region //------ITEM DATA------//
    // The itemSO that this item pulls its data from
    public ItemSO itemSO;

    // Number of items collected when picking up the item
    // For now, it's set to 1 at the start, but this could perhaps be dynamically set in the spawnManager in the future?
    public int quantity = 1;
    #endregion

    #region //------COMPONENTS AND SCENE OBJECTS------//

    // Scene Objects
    private InventoryManager inventoryManager;

    // Item Components
    [SerializeField] private GameObject spriteObject; // Child object, assigned in Unity editor
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider;

    #endregion

    void Awake()
    {
        // Set all scene objects & components
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
    }
    
    // Item is picked up when the player walks over it
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

    // Properly sizes the sprite
    private void FitSpriteToCollider()
    {
        // Gets the size of the sprite & box collider
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector2 colliderSize = boxCollider.size;

        // Calculates scale needed to fit sprite within collider
        float scaleX = colliderSize.x / spriteSize.x;
        float scaleY = colliderSize.y / spriteSize.y;

        // Applies scale to the sprite while maintaining the aspect ration
        float scale = Mathf.Min(scaleX, scaleY);
        spriteObject.transform.localScale = new Vector3(scale, scale, 1f);
    }

    // When called (by spawnmanager) sends the proper itemSO data to this object
    public void PassItemSOData(ItemSO itemSO)
    {
        // Passes along the proper itemSO
        this.itemSO = itemSO;

        // Sets the sprite & proper size
        spriteRenderer.sprite = itemSO.sprite;
        FitSpriteToCollider();
    }
}