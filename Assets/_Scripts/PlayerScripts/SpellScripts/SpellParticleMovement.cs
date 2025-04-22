/*---------------------------------------- BY LINA ----------------------------------------
------------------------------- CURRENTLY UNUSED IN GAME ----------------------------------

An old tester script used in conjunction with ParticleConeSpawner, which spawns in a bunch of particle prefabs with this script attached.
On instantiation, this script simply moves the particle in a random direction (as given by ParticleConeSpawner)
until it reaches its maximum distance (also as determined by ParticleConeSpawner).

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellParticleMovement : MonoBehaviour
{
    // All given by ParticleConeSpawner
    private Vector3 moveDirection;
    private float maxDistance;
    private Vector3 startPosition;

    public void Initialize(Vector3 direction, float distance)
    {
        moveDirection = direction.normalized;
        maxDistance = distance;
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += moveDirection * Time.deltaTime;

        // Despawns the particle at maxDistance
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}