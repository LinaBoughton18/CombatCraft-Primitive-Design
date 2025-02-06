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

    public int health;

    public List<ItemTuple> itemsDropped;


    // WILL store: weaknesses/resistances/immmunities
    // MIGHT also store: enemy attackset, enemy movement set, enemy finite state machine???

}