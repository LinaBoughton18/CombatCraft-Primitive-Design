/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

Used for a tester spell-particle, one that stays still & deletes itself after 10 seconds.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSpellParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 10f); // Destroy after 10 seconds
    }
}
