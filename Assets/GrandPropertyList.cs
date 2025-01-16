using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class that contains basic data related stuff about the properties & their data
public class GrandPropertyList
{
    // These are the possible shapes, the spellPrefab can handle the functions from there
    public enum Shape
    {
        entity,
        directTarget,
        projectile_circle,
        wall_wallDrawn,
        cone_circle,
        arc_circle,
        beam_circle
    };

    // Effects applied to a person or object for a certain amount of time
    public enum Condition
    {
        onFire, poisoned, blinded, increaseSpeed, decreaseSpeed
    };

    // Effects that edit health
    public enum Damage
    {
        fire, poison, death
    };


}