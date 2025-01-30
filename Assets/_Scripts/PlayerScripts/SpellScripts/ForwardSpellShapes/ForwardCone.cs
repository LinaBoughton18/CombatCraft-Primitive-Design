using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardCone : MonoBehaviour, ISpellShape
{
    // LATER, THESE OUGHT TO CHANGE DYNAMICALLY, NOT BE SET AT THESE VALUES
    [Header("Cone Settings")]
    public float coneAngle = 30f; // Half of the cone's total angle
    public float coneLength = 5f; // Maximum distance the particles will travel
    public int particleCount = 50; // Number of particles to spawn

    [Header("Particle Settings")]
    public GameObject particlePrefab; // Prefab for the particle

    public void Execute()
    {
        SpawnParticles();
    }

    void SpawnParticles()
    {
        // Get the player's position
        Vector3 playerPosition = transform.position;

        // Calculate the direction to the mouse
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure the Z position matches the player's 2D plane
        Vector3 coneDirection = (mouseWorldPosition - playerPosition).normalized;

        // Spawn particles within the cone
        for (int i = 0; i < particleCount; i++)
        {
            // Generate a random direction within the cone
            Vector3 randomDirection = GetRandomDirectionInCone(coneDirection, coneAngle);

            // Instantiate the particle
            GameObject particle = Instantiate(particlePrefab, playerPosition, Quaternion.identity);

            // Get the ParticleMover component and set up its movement
            SpellParticleMovement mover = particle.GetComponent<SpellParticleMovement>();
            if (mover != null)
            {
                mover.Initialize(randomDirection, coneLength);
            }
        }
    }

    Vector3 GetRandomDirectionInCone(Vector3 coneDirection, float angle)
    {
        // Generate a random rotation within the cone
        float randomAngle = Random.Range(-angle, angle);
        Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.forward);

        // Apply the rotation to the cone direction
        return rotation * coneDirection;
    }

}
