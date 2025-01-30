using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    #region //------SCENE OBJECTS------//
    //------SCENE OBJECTS------//
    private GameObject player;
    private SpellManager spellManager;
    private InventoryManager inventoryManager;
    #endregion

    #region //-----SPELL ITEM LIST-----//
    [SerializeField] private List<ItemSO> itemList = new List<ItemSO>(); // The list of items used in this spell
    #endregion

    #region //------SPELL PROPERTY LISTS------//
    // Pulling from GrandPropertyList (where all the property types are stored)
    // these are the properties in the spell
    [SerializeField] private List<GrandPropertyList.Condition> conditionList = new List<GrandPropertyList.Condition>();
    [SerializeField] private List<GrandPropertyList.Damage> damageList = new List<GrandPropertyList.Damage>();

    [SerializeField] private int castingType;
    [SerializeField] private GrandPropertyList.Shape shape;
    #endregion

    #region //------SPELL SHAPE INTERFACES------//
    private ISpellShape spellShape;
    #endregion

    #region //------ADDITIONAL SPELL PROPERTIES------//
    [SerializeField] private Vector2 origin; // Determines the starting point of the spell
    [SerializeField] private Vector2 mousePosition; // Can be used to calculate the direction of the spell
    [SerializeField] private Vector2 direction; // The direction the spell moves in (if at all)
    [SerializeField] private float scale; // The size of the spell, determined by amount of inputs
    #endregion

    //------METHODS------//
    void Awake()
    {
        player = GameObject.Find("Player");
        spellManager = player.GetComponent<SpellManager>();
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    // Grabs the items from the spellQueue and puts them in the itemList
    public void PassInfo(ItemSlot[] spellQueue, int castingType)
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
        this.castingType = castingType; // Determines if this is a forward, area, or self casted spell
        DetermineProperties();
        DetermineOriginAndMovement();
        DetermineScale();

        //PrintSpellProperties();

        // Once all the properties have been collected, spawn the spell in
        CreateSpell();
    }

    #region //-----------DETERMINING SPELL PROPERTIES-----------//
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
    #endregion

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

    private void CreateSpell()
    {
        // Shape is the most defining thing... I think
        // Instantiate a script and/or nab the appropriate shape-based behavior (for forward casted spells)
        // ^ Add in the proper origin, scale, direction, speed, etc.
        // Pull the proper sprites & other info from the damages & conditions (damages & conditions shall be their own SO's now!)

        // Spawn in the spell particles (reference behavior from ParticleConeSpawner)
        // (Spell particles reference SpellParticleMovement)



        // Grab appropriate behavior from a script to spawn the particles (Interface?)
        // 


        // Spell: spell class, replaces forwardspellPrefab
        // ISpellShape: the interface for different spell shapes
        // ForwardConeShape, ForwardBeamShape, ForwardCircleShape, etc: the interfaces that use the spell shape behavior & execute them

        //TODO
        // HOW DO WE WANT TO STORE SPELLSHAPES??? AS SO's perhaps? 
        // WE NEED TO FIGURE OUT HOW TO ASSIGN SPELLSHAPE in order to execute it!!!!
        spellShape.Execute();
    }








    /*public class ForwardSpellPrefab : MonoBehaviour
    {
        // Creates a list to store all the behaviors that will be added to the spell
        private List<ISpellShape> behaviors = new List<ISpellShape>();

        // Takes a grouping of something (behaviorTypes) & grabs their proper behavior each time
        public void Initialize(List<Type> behaviorTypes)
        {
            // Loops through the list given, grabbing the interface & adding it to our list
            foreach (Type type in behaviorTypes)
            {
                if (typeof(ISpellShape).IsAssignableFrom(type))
                {
                    ISpellShape behavior = gameObject.AddComponent(type) as ISpellShape;
                    
                    behaviors.Add(behavior);
                }
            }

            // Executes all behaviors in the list
            ExecuteBehaviors();
        }

        // Executes each behavior in the behaviors list
        private void ExecuteBehaviors()
        {
            foreach (ISpellShape behavior in behaviors)
            {
                behavior.Execute(this);
            }
        }
    }







    */
}