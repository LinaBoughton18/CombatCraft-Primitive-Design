using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Everything we need to deal with health & death
// Because it's not attatched to a game object, we can get rid of the usual "MonoBehaviour"
// It's also not a class, it's an interface
    // Interfaces set up interfaces for us to work with in the Unity Editor
    // Interfaces don't really implement anything specific
public interface IDamagable
{
    void Damage(float damageAmount);

    void Die();

    float maxHealth { get; set; }
    float currentHealth { get; set; }
}