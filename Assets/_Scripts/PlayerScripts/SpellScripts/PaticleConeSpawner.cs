/*---------------------------------------- BY LINA ----------------------------------------
------------------------------- CURRENTLY UNUSED IN GAME  ---------------------------------

This script is an old tester script used to spawn particles in a cone shape (or an arc, more accurately).
Used in conjunction with the SpellParticleMovement script (also unused).
Most of its functionality have been relocated into the Spell class, but this script is still around for testing purposes

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleConeSpawner : MonoBehaviour
{
    // MOST FUNCTIONS OF THIS SCRIPT ARE BEING RELOCATED INTO SPELL & ITS INTERFACES
    // THIS IS JUST FOR TESTING
    
    // Cone Variables
    public float coneAngle = 30f; // Half the cone's angle!! (to make it even)
    public float coneLength = 5f;
    public int particleCount = 50;
    public GameObject particlePrefab;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnParticles();
        }
    }

    void SpawnParticles()
    {
        // Calculates direction from player to the moues
        Vector3 playerPosition = transform.position;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        Vector3 coneDirection = (mouseWorldPosition - playerPosition).normalized;

        // Spawns particles within the cone
        for (int i = 0; i < particleCount; i++)
        {
            // Creates a random direction in the cone
            Vector3 randomDirection = GetRandomDirectionInCone(coneDirection, coneAngle);

            // Instantiate the particle & set it to move in the random direction
            GameObject particle = Instantiate(particlePrefab, playerPosition, Quaternion.identity);
            SpellParticleMovement mover = particle.GetComponent<SpellParticleMovement>();
            if (mover != null)
            {
                mover.Initialize(randomDirection, coneLength);
            }
        }
    }

    // Creates a random direction in the cone
    Vector3 GetRandomDirectionInCone(Vector3 coneDirection, float angle)
    {
        float randomAngle = Random.Range(-angle, angle);
        Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);

        return rotation * coneDirection;
    }
}