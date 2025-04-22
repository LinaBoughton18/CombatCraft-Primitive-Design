/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

Stores all unique information about an enemy (name, sprite/image (can include animations in the future), etc.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySO : ScriptableObject
{
    public string enemyName;

    public Sprite enemySprite; // This'll probably get more compliacted later once I add in animations & such

    public int damageDealt; // upon hitting the player (THIS IS A PLACEHOLDER)

    public int speed;

    public int maxHealth;

    public List<ItemTuple> itemsDropped;


    // WILL LATER store: weaknesses/resistances/immmunities, enemy attackset, enemy movement set, enemy finite state machine(?)

}