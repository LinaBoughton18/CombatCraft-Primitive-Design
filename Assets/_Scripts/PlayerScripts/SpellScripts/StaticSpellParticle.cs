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
