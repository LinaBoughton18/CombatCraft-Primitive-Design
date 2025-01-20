using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ForwardSpellPrefab : MonoBehaviour
{
    //------SCENE OBJECTS------//
    private GameObject player;
    private SpellManager spellManager;
    private InventoryManager inventoryManager;

    //-----SPELL ITEM LIST-----//
    [SerializeField] private List<ItemSO> itemList = new List<ItemSO>(); // The list of items used in this spell

    //------SPELL PROPERTY LISTS------//
    // Pulling from GrandPropertyList (where all the property types are stored)
    // these are the properties in the spell
    [SerializeField] private List<GrandPropertyList.Condition> conditionList = new List<GrandPropertyList.Condition>();
    [SerializeField] private List<GrandPropertyList.Damage> damageList = new List<GrandPropertyList.Damage>();
    [SerializeField] private GrandPropertyList.Shape shape;

    //------ADDITIONAL SPELL PROPERTIES------//
    [SerializeField] private Vector2 origin; // Determines the starting point of the spell
    [SerializeField] private Vector2 mousePosition; // Can be used to calculate the direction of the spell
    [SerializeField] private Vector2 direction; // The direction the spell moves in (if at all)
    [SerializeField] private float scale; // The size of the spell, determined by amount of inputs

    //------METHODS------//
    void Awake()
    {
        player = GameObject.Find("Player");
        spellManager = player.GetComponent<SpellManager>();
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Grabs the items from the spellQueue and puts them in the itemList
    public void PassItems(ItemSlot[] spellQueue)
    {
        // Create a list of the items (in ItemSO form) in this spell
        // Iterate through spellQueue, add each non-null element to itemList
        foreach (ItemSlot i in spellQueue)
        {
            if (i != null)
            {
                itemList.Add(i.itemSO);
            }
            else
            {
                break;
            }
        }
        itemList.TrimExcess(); // Removes the excess memory

        // Sets the spells various magical & spacial properties
        DetermineProperties();
        DetermineOriginAndMovement();
        DetermineScale();

        //PrintSpellProperties();
    }

    // Determine damage, conditions, and shape
    void DetermineProperties()
    {
        // (For conditionList & damageList, iterate through each using foreach
        // Iterate through itemList, grab each item's conditions & damages respectively)

        // Create the list of spellProperties

        // Start by setting the default shape to the lowest priority
        shape = GrandPropertyList.Shape.beam_circle;

        // Iterate through each item in itemList, updating the damage, conditions, and shape as you go
        foreach (ItemSO i in itemList)
        {
            // Iterate through every condition in that item's conditionList
            // If that condition is not already in this spell's conditionList, add it
            foreach (GrandPropertyList.Condition j in i.conditionList)
            {
                if (!this.conditionList.Contains(j))
                {
                    this.conditionList.Add(j);
                }
            }

            // Iterate through every damage in that item's damageList
            // If that damage is not already in this spell's conditionList, add it
            foreach (GrandPropertyList.Damage j in i.conditionList)
            {
                if (!this.damageList.Contains(j))
                {
                    this.damageList.Add(j);
                }
            }

            // Check the shape property for this item in itemList
            // If the shapeToTest has greater priority than the current shape, change it to the new shape
            GrandPropertyList.Shape shapeToTest = i.shape;
            if ((int)shapeToTest < (int)this.shape)
            {
                this.shape = shapeToTest;
            }
        }
    }

    // Sets the origin & direction for the spell
    // Since this is forward casted, the origin & direction is handeled by the player & mouse positions
    void DetermineOriginAndMovement()
    {
        // Origin = player position (might need to be slightly offset to prevent the player from hitting themselves)
        origin = player.transform.position;

        // Direction = towards mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - origin).normalized;
    }

    void DetermineScale()
    {
        // For now, scale is the number of items put into a spell
        scale = itemList.Count;
    }

    /*
    2 problems to fix right now:
    1) How are spells going to be spawned?
    Spells have inherient properties: origin, direction, shape, size/range, & effects
    Forward casted spells automatically handle origin (player) & direction (towards mouse), leaving us with shape, size/range, and effects
    Area casted spells automatically handle origin (mouse), leaving us with direction, shape, size/range, and effects
    Self casted spells automatically handle origin (player), and disregard direction & shape, leaving us with size/range (or amount) and effects

    2) How are properties going to be added to spells?
    The items are stored as scriptable objects, with their properties being variables in those scriptable objects.
    The spells are prefabs, which instantiate with all sorts of various effects.
    When is a spell is cast, ItemSlots pass the information from their item, to the inventory manager, to the spell queue, to the spell prefab.
    From there, the properties are grabbed and calculations are done accordingly.

    For now, spell effects will just be strings. I'll figure the rest of that out later.

*/

    // A debug message that prints out the properties of the spell
    private void PrintSpellProperties()
    {
        Debug.Log("-----------SPELL ITEMS-----------");
        StockDebug.PrintArray(itemList.ToArray(), i => i, "", false);

        Debug.Log("-----------SPELL PROPERTIES-----------");
        StockDebug.PrintArray(conditionList.ToArray(), s => s, "Conditions: ", false);
        StockDebug.PrintArray(damageList.ToArray(), d => d, "Damages: ", false);

        Debug.Log("Shape " + shape);

        Debug.Log("Origin: " + origin + " (should match the player position)");
        Debug.Log("Direction: " + direction);
        Debug.Log("Scale: " + scale);
    }
}