using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SO means scriptable object, a "data container"
// They're not MonoBehaviors, their data persists between playthoughs
// A scriptable object is not a singular object in the game, it's more like
// a basis for creating other object (I think)
// SO's do not use the Start and Update functions, you create your own!

[CreateAssetMenu] // Allows you to create instances of this SO by
// clicking "Create" in Unity, so we can easily create additional objects
// These items are stored in the assets folder, under "Items"
public class ItemSO : ScriptableObject
{
    //-----MISC ITEM VARIABLES-----//
    public string itemName;

    // The item's image sprite
    public Sprite sprite;

    // The description of the item in the inventory
    [TextArea] // This line gives us a big box in the Unity Editor in which we can edit the text!
    public string itemDescription;

    //-----ITEM PROPERTIES-----//
    /*
    Since we want everything to be decoupled (not dependent on each other), the classes will call their own methods & pass them along to each other
    The spellPrefab will take the shape, which is passsed along and it doesn't care how
    */

    // Can edit in the Unity Editor
    //public GrandPropertyList.Shape shape;

    public SpellShapeSO shape;

    public GrandPropertyList.Condition[] conditionList;

    public GrandPropertyList.Damage[] damageList;

    // I might add more types of lists later. The properties should be sorted based on when the enemies (or elements in the environment) call them.
    // I can also add methods here if I need to.

}






/*
// This is some old code from the original inventory tutorial.
It has two variables, a stat to change and how much to change it by, which is activated when the inventory item is clicked
I'll be using something else for me :)

public StatToChange statToChange = new StatToChange();
public enum StatToChange
{
    // These can be different as I improve my game
    none,
    health,
    mana,
    stamina
}; 
public int amountToChangeStat;

// Called everytime we want to use an item
public bool UseItem()
{
    if (statToChange == StatToChange.health)
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        if (playerController.playerHealth == playerController.maxPlayerHealth)
        {
            return false;
        }
        else
        {
            playerController.ChangeHealth(amountToChangeStat);
            return true;
        }
    }
    return false;
}
*/
