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
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
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

    // Enumeration: allow you to create drop-down menus of related constants
    // It's like a list-variable with specific, predetermined options to choose from
    public enum StatToChange
    {
        // These can be different as I improve my game
        none,
        health,
        mana,
        stamina
    }; // Note the semicolon!
}
