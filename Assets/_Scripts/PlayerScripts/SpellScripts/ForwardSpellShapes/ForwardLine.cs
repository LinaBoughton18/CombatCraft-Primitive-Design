using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardLine : MonoBehaviour, ISpellShape
{
    [Header("Beam Settings")]
    public float beamWidth = 5f; // The beam's total width CURRENTLY UNUSED!
    public float coneLength = 5f; // Maximum distance the particles will travel
    public int particleCount = 50; // Number of particles to spawn

    [Header("Particle Settings")]
    public GameObject particlePrefab; // Prefab for the particle

    public void Execute()
    {
        StartCoroutine(SpawnParticles());
    }

    IEnumerator SpawnParticles()
    {
        // Get the player's position
        Vector3 playerPosition = transform.position;

        // Calculate the direction to the mouse
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure the Z position matches the player's 2D plane
        Vector3 beamDirection = (mouseWorldPosition - playerPosition).normalized;

        // Spawn particles within the beam
        for (int i = 0; i < particleCount; i++)
        {
            // Instantiate the particle
            GameObject particle = Instantiate(particlePrefab, playerPosition, Quaternion.identity);

            // Get the ParticleMover component and set up its movement
            SpellParticleMovement mover = particle.GetComponent<SpellParticleMovement>();
            if (mover != null)
            {
                mover.Initialize(beamDirection, coneLength);
            }
            yield return new WaitForSeconds(.01f);
        }
    }
}
