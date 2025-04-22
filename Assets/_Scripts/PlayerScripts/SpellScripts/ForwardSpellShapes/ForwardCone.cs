/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

A child class of SpellShapeSO -> spawns spell particles in a cone.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ForwardCone : SpellShapeSO
{
    // LATER, these can be changed dynamically (by the Spell class)
    public float coneAngle = 30f; // Half of the cone's total angle
    public float coneLength = 5f; // Maximum distance the particles will travel
    public int particleCount = 50; // Number of particles to spawn
    public GameObject particlePrefab; // Prefab for the particle

    public void Execute()
    {
        SpawnParticles();
    }

    void SpawnParticles()
    {
        // Gets the player's position
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        // Calculates the direction to the player's moues
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        Vector3 coneDirection = (mouseWorldPosition - playerPosition).normalized;

        // Spawns particles within the cone
        for (int i = 0; i < particleCount; i++)
        {
            // Generates a random direction within the cone
            Vector3 randomDirection = GetRandomDirectionInCone(coneDirection, coneAngle);

            // Instantiates the particle
            GameObject particle = Instantiate(particlePrefab, playerPosition, Quaternion.identity);

            // Gets the ParticleMover component and sets up  movement
            SpellParticleMovement mover = particle.GetComponent<SpellParticleMovement>();
            if (mover != null)
            {
                mover.Initialize(randomDirection, coneLength);
            }
        }
    }

    Vector3 GetRandomDirectionInCone(Vector3 coneDirection, float angle)
    {
        // Generates a random direction within the cone
        float randomAngle = Random.Range(-angle, angle);
        Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);
        return rotation * coneDirection;
    }

}
