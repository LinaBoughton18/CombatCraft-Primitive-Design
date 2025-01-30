using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A class that contains basic data related stuff about the properties & their data
public class GrandPropertyList
{
    // These are the possible shapes, the spellPrefab can handle the functions from there
    // In order of priority (first to last)
    public enum Shape
    {
        entity = 1,
        directTarget = 2,
        projectile_circle = 3,
        wall_wallDrawn = 4,
        cone_circle = 5,
        arc_circle = 6,
        beam_circle = 7
    };

    // Effects applied to a person or object for a certain amount of time
    public enum Condition
    {
        onFire, poisoned, blinded, increaseSpeed, decreaseSpeed
    };

    // Effects that edit health
    public enum Damage
    {
        fire, poison, death, ice, light, earth, metal
    };


}