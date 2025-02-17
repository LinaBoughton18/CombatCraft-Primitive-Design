using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    // TESTING PURPOSES ONLY
    public GameObject particlePrefab;

    private InventoryManager inventoryManager;

    // The spellQueue, holds references to item slots w/ the items in there
    // By default, sets to [null, null, null, null, null]
    static int spellQueueSize = 5;
    public ItemSlot[] spellQueue = new ItemSlot[spellQueueSize];

    // The spellQueue UI, updates to match the spellQueue
    public SpellQueueSlot[] spellQueueSlot;

    // A prefab for the casted spells
    public Spell spell;

    private KeyCode forwardCastKey = KeyCode.Mouse0;
    private KeyCode areaCastKey = KeyCode.Mouse1;
    private KeyCode selfCastKey = KeyCode.Mouse2;
    private KeyCode[] castingKeys;

    // Helps with spell!
    public SpellShapeSO defaultSpellShape; // Lowest ranking spellShape! (Assigned in Unity Editor!)

    void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();

        castingKeys = new KeyCode[] { forwardCastKey, areaCastKey, selfCastKey };
    }

    void Update()
    {
        SpellPrepAndCast();
    }

    // THERE ARE EDITS TO DO WITHIN
    public void SpellPrepAndCast()
    {
        #region //----------SPELL PREP----------//
        // Adds or removes items from the SpellQueue
        // Checks for keys #1-#5 down & if those are in the inventory -> Adds components to the spell if available
        // If the corresponding inventory item is already in the spellQueue -> removes the item

        // spellQueueSize = 5
        // runs as keyDown = 1, 2, 3, 4, 5. Done.
        for (int keyDown = 1; keyDown <= spellQueueSize; keyDown++)
        {
            // Checks if a key has been pressed
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), "Alpha" + (keyDown))))
            {
                // Finds the correct itemSlot we want to reference
                int HUDIndex = keyDown - 1; // The index of the HUD corresponding to our key down
                ItemSlot correspondingItemSlot = inventoryManager.itemSlot[HUDIndex + inventoryManager.HUDRowSelected];

                // If the item has not been added when the key is pressed -> add the item to the spellQueue
                if (!correspondingItemSlot.isInQueue)
                {
                    // Adds the item in the corresponding inventory slot (i + HUDROWSelected (done in inventoryManager))
                    // to the first spot in the SpellQueue. That spot in SpellQueue now points to
                    AddToSpellQueue(HUDIndex);
                }
                // If the item is already in the spellQueue -> remove it
                else
                {
                    RemoveFromSpellQueue(HUDIndex);
                }
            }
        }
        #endregion

        #region //----------SPELL CAST----------//
        // Casts the spell -> forward, area, self casting
        for (int i = 0; i < castingKeys.Length; i++)
        {
            if (Input.GetKeyDown(castingKeys[i]))
            {
                if (!SpellQueueEmpty())
                {
                    // Casts the spell, spawning in a spell object
                    SpellCast(i);

                    // Decrement the quantities of the items from the inventory
                    inventoryManager.DecrementItemsInSpell(spellQueue);

                    // Clears the spellQueue
                    ClearSpellQueue();
                }
            }
        }
        #endregion

    }

    // Adds the itemSlotToAdd to the first available slot of the spellQueue
    public void AddToSpellQueue(int correspondingHUDSlotToAdd)
    {
        // Iterates through the SpellQueue, finding the first available null spot to add the item into
        // If all spots in SpellQueue are full, then does nothing
        for (int i = 0; i < spellQueueSize; i++)
        {
            if (spellQueue[i] == null)
            {
                spellQueue[i] = inventoryManager.QueueItem(correspondingHUDSlotToAdd);

                UpdateSpellQueueUI();
                return;
            }
        }
    }

    // Removes the item from spellQueue
    // spellQueueIndex is wrong!!!! It's been changed up in SpellPrepState to HUDIndex, since that's actually what we're referencing
    public void RemoveFromSpellQueue(int HUDIndex)
    {
        // So we have our HUDIndex, which is the corresponding HUD slot (0-4) which corresponds to the key we just pressed
        // itemSlotToRemove is the corresponding itemSlot in the inventoryManager
        ItemSlot itemSlotToRemove = inventoryManager.itemSlot[HUDIndex + inventoryManager.HUDRowSelected];
        
        // Iterate through spellQueue to find the item slot we want to remove
        for (int i = 0; i < spellQueueSize; i++)
        {
            // Once we've found the correct item to remove
            if (spellQueue[i] == itemSlotToRemove)
            {
                // Removes the queued status from the itemslot & sets the spellQueue index to null
                spellQueue[i] = inventoryManager.DequeueItem(HUDIndex);

                // If the item removed is NOT the last item in the array, move all other items forward
                if (i != (spellQueueSize - 1))
                {
                    for (int j = i; j < (spellQueueSize - 1); j++)
                    {
                        spellQueue[j] = spellQueue[j + 1];
                    }
                    // Sets the last item in the array to null. (Because we've just removed an item, the last item must be null!)
                    spellQueue[spellQueueSize - 1] = null;
                }
                
                UpdateSpellQueueUI();
                break;
            }
        }
    }

    // Removes all itemSlot references from the spellQueue
    public void ClearSpellQueue()
    {
        for (int i = 0; i < spellQueue.Length; i++)
        {
            spellQueue[i] = null;
        }

        UpdateSpellQueueUI();
    }

    public void SpellCast(int castingType)
    {
        Spell newSpell = Instantiate(spell, new Vector2(0, 0), Quaternion.identity);
        newSpell.PassInfo(spellQueue, castingType);
    }

    public void UpdateSpellQueueUI()
    {
        //Debug.Log("Hello from UpdateSpellQueueUI");
        for (int i = 0; i < spellQueueSize; i++)
        {
            // Update the UISlot to match the same index in the spellQueue
            // Iterates through spellQueueSlots, editing the picture to match the corresponding inventory row
            // Does this by prompting each HUDItemSlot to edit itself, passing the corresponding inventory slot as reference
            this.spellQueueSlot[i].UpdateSpellQueueSlotUI(spellQueue[i]);
        }
    }

    /*
    2 problems to fix right now:
    1) How are spells going to be spawned?
    Items = scriptable objects (immutable, data storage)
    Spells = prefabs (contain mutable data, might need to spawn in different things)


    Spells have inherient properties: origin, direction, shape, size/range, & effects
    Forward casted spells automatically handle origin (player) & direction (towards mouse), leaving us with shape, size/range, and effects
    Area casted spells automatically handle origin (mouse), leaving us with direction, shape, size/range, and effects
    Self casted spells automatically handle origin (player), and disregard direction & shape, leaving us with size/range (or amount) and effects

    2) How are properties going to be added to spells?
    The items & their properties are stored as scriptable objects.
    Inventory manager passes the items from the ItemSlots, to the InventoryManager, to the SpellQueue, to the spell function in the queue.


    */

    // Returns false if spell queue is not empty, true if it is empty
    public bool SpellQueueEmpty()
    {
        for (int i = 0; i < spellQueueSize; i++)
        {
            if (spellQueue[i] != null)
            {
                return false;
            }
        }
        return true;
    }

}