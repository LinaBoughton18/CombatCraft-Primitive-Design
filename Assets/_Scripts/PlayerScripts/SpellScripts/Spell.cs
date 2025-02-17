using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    // TESTING PARTICLE
    public GameObject particlePrefab; // Prefab for the particle
    public GameObject staticSpellParticle; // For the Wall spell

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

    [SerializeField] private List<DamageSO> newDamageList = new List<DamageSO>();
    [SerializeField] private List<ConditionSO> newConditionList = new List<ConditionSO>();

    [SerializeField] private int castingType;
    // VERSION 1: SHAPE AS AN ENUMERATION
    [SerializeField] private GrandPropertyList.Shape shape;
    // VERSION 2: SHAPE AS A SCRIPTABLE OBJECT
    [SerializeField] private SpellShapeSO shape2;

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
        spellManager = player.GetComponentInChildren<SpellManager>();
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

        // Assigns shape to automatically be the lowest priority shape
        shape = GrandPropertyList.Shape.beam_circle;
        //Debug.Log("initial shape = " + shape);
        shape2 = spellManager.defaultSpellShape;
        //Debug.Log("shape assigned! shape = " + shape2.name);

        // Iterate through each item in itemList, updating the damage, conditions, and shape as you go
        foreach (ItemSO i in itemList)
        {
            //Debug.Log("Checking item: " + i.itemName);

            // CONDITIONS
            // Iterate through every condition in that item's conditionList
            // If that condition is not already in this spell's conditionList, add it
            foreach (GrandPropertyList.Condition j in i.conditionList)
            {
                if (!this.conditionList.Contains(j))
                {
                    this.conditionList.Add(j);
                }
            }

            // DAMAGES
            // Iterate through every damage in that item's damageList
            // If that damage is not already in this spell's conditionList, add it
            foreach (GrandPropertyList.Damage j in i.damageList)
            {
                if (!this.damageList.Contains(j))
                {
                    this.damageList.Add(j);
                }
            }

            #region SHAPE

            #region VERSION 1

            GrandPropertyList.Shape shapeToTry = i.shapeEnum;

            //Debug.Log("New shape to try = " + shapeToTry);

            if ((int)shapeToTry < (int)this.shape)
            {
                this.shape = shapeToTry;
            }
            //Debug.Log("New shape = " + this.shape);

            // NEW--------------------------


            #endregion

            #region VERSION 2

            // Check the shape property for this item in itemList
            // If the shapeToTest has greater priority than the current shape, change it to the new shape
            SpellShapeSO shapeToTest = i.shape; // Grab's i's spellShapeSO

            //Debug.Log("i.shape = " + i.shape);       //OK so the current problem is that i.shape isn't set to anything
            // This means that certain ItemSOs don't have shapes assigned to them, & that's something I can fix
            //Debug.Log("shapeToTest = " + shapeToTest);

            //Debug.Log("(int)shapeToTest.shape = " + (int)shapeToTest.shape);
            //Debug.Log("(int)this.shape.shape = " + (int)this.shape2.shape);

            if ((int)shapeToTest.shape < (int)this.shape2.shape)
            {
                this.shape2 = shapeToTest;
            }

            #endregion

            #endregion
            
            // NEW --------------------------------------------
            foreach (DamageSO damage in i.newDamageList)
            {
                this.newDamageList.Add(damage);
            }
            foreach (ConditionSO condition in i.newConditionList)
            {
                this.newConditionList.Add(condition);
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

    #endregion //----------------------------------------------//

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
        // Instantiate a script and/or nab the appropriate shape-based behavior (for forward casted spells)
        // ^ Add in the proper origin, scale, direction, speed, etc.
        // Pull the proper sprites & other info from the damages & conditions (damages & conditions shall be their own SO's now!)

        // Spawn in the spell particles (reference behavior from ParticleConeSpawner)
        // (Spell particles reference SpellParticleMovement)

        // Spell: spell class, replaces forwardspellPrefab
        // ISpellShape: the interface for different spell shapes
        // ForwardConeShape, ForwardBeamShape, ForwardCircleShape, etc: the interfaces that use the spell shape behavior & execute them

        //shape2.concreteShapeBehavior.Execute();


        // Executes the shape of the spell----------------------------------------------
        // I'm fully aware that this method is poor, but it works for now!
        Debug.Log("Casting shape " + shape);
        switch ((int)shape)
        {
            case 1:
                Entity();
                break;
            case 2:
                DirectTarget();
                break;
            case 3:
                Projectile();
                break;
            case 4:
                Wall();
                break;
            case 5:
                Cone();
                break;
            case 6:
                Arc();
                break;
            case 7:
                StartCoroutine(Line());
                break;
        }
        
    }

    //Needs work
    #region ENTITY

    private void Entity()
    {

    }

    #endregion

    //Needs work
    #region DIRECT TARGET

    private void DirectTarget()
    {

    }

    #endregion

    #region PROJECTILE

    private void Projectile()
    {
        float beamWidth = 5f; // The beam's total width CURRENTLY UNUSED!
        float projectileLength = 20f; // Maximum distance the particles will travel
        int particleCount = 1; // Number of particles to spawn

        // Get the player's position
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        // Calculate the direction to the mouse
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure the Z position matches the player's 2D plane
        Vector3 beamDirection = (mouseWorldPosition - playerPosition).normalized;

        // Spawn particles within the beam
        for (int i = 0; i < particleCount; i++)
        {
            // Instantiate the particle
            GameObject particle = Instantiate(particlePrefab, playerPosition, Quaternion.identity);
            particle.transform.localScale += new Vector3(4, 4, 4);

            // Get the ParticleMover component and set up its movement
            SpellParticleMovement mover = particle.GetComponent<SpellParticleMovement>();
            if (mover != null)
            {
                mover.Initialize(beamDirection, projectileLength);
            }

        }
    }

    #endregion

    #region WALL

    private void Wall()
    {
        int spawnCount = 7; // Number of prefabs to spawn
        float spacing = .5f; // Distance between each spawned object
        float spawnDistance = 1.0f; // Fixed distance from the player

        transform.position = player.transform.position;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector3 directionToMouse = (mouseWorldPosition - transform.position).normalized;
        Vector3 spawnCenter = transform.position + directionToMouse * spawnDistance;

        Vector3 perpendicular = new Vector3(-directionToMouse.y, directionToMouse.x, 0f); // Perpendicular direction

        for (int i = 0; i < spawnCount; i++)
        {
            float offset = (i - (spawnCount - 1) / 2.0f) * spacing;
            Vector3 spawnPosition = spawnCenter + perpendicular * offset;
            Instantiate(staticSpellParticle, spawnPosition, Quaternion.identity);
        }

    }

    #endregion

    #region CONE

    private void Cone()
    {
        // Cone Settings
        float coneAngle = 30f; // Half of the cone's total angle
        float coneLength = 5f; // Maximum distance the particles will travel
        int particleCount = 30; // Number of particles to spawn

        // Calculate the direction to the mouse
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure the Z position matches the player's 2D plane
        Vector3 coneDirection = (mouseWorldPosition - (new Vector3(origin.x, origin.y, 0))).normalized;

        for (int i = 0; i < particleCount; i++)
        {
            // Generate a random direction within the cone
            Vector3 randomDirection = GetRandomDirectionInCone(coneDirection, coneAngle);

            // Instantiate the particle
            GameObject particle = Instantiate(particlePrefab, origin, Quaternion.identity);

            // Get the ParticleMover component and set up its movement
            SpellParticleMovement mover = particle.GetComponent<SpellParticleMovement>();
            if (mover != null)
            {
                mover.Initialize(randomDirection, coneLength);
            }
        }
    }

    Vector3 GetRandomDirectionInCone(Vector3 coneDirection, float angle)
    {
        // Generate a random rotation within the cone
        float randomAngle = Random.Range(-angle, angle);
        Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);

        // Apply the rotation to the cone direction
        return rotation * coneDirection;
    }

    #endregion

    #region ARC

    private void Arc()
    {
        Cone();
    }

    #endregion

    #region LINE

    private IEnumerator Line()
    {
        float beamWidth = 5f; // The beam's total width CURRENTLY UNUSED!
        float coneLength = 5f; // Maximum distance the particles will travel
        int particleCount = 50; // Number of particles to spawn

        // Get the player's position
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        // Calculate the direction to the mouse
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure the Z position matches the player's 2D plane
        Vector3 beamDirection = (mouseWorldPosition - playerPosition).normalized;

        // Spawn particles within the beam
        for (int i = 0; i < particleCount; i++)
        {
            // Instantiate the particle
            GameObject particle = Instantiate(particlePrefab, playerPosition, Quaternion.identity);

            // Get the ParticleMover component and set up its movement
            SpellParticleMovement mover = particle.GetComponent<SpellParticleMovement>();
            if (mover != null)
            {
                mover.Initialize(beamDirection, coneLength);
            }
            yield return new WaitForSeconds(.1f);

        }
    }

    #endregion








    /*
    public class ForwardSpellPrefab : MonoBehaviour
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