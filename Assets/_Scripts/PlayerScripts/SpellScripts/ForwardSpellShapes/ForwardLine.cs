/*---------------------------------------- BY LINA ----------------------------------------
-------------------------------------------------------------------------------------------

A child class of SpellShapeSO -> spawns spell particles in a straight line.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ForwardLine : SpellShapeSO
{
    public float beamWidth = 5f; // The beam's total width (CURRENTLY UNUSED!)
    public float coneLength = 5f; // Maximum distance the particles will travel
    public int particleCount = 50; // Number of particles to spawn
    public GameObject particlePrefab; // Prefab for the particle

    public void Execute()
    {
        //StartCoroutine(SpawnParticles());
        SpawnParticles();
    }

    IEnumerator SpawnParticles()
    {
        // Gets the player's position
        Vector3 playerPosition = GameObject.Find("Player").transform.position;

        // Calculates the direction to the moues
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f; // Ensure the Z position matches the player's 2D plane
        Vector3 beamDirection = (mouseWorldPosition - playerPosition).normalized;
        
        // Spawns particles in the beam
        for (int i = 0; i < particleCount; i++)
        {
            // Instantiates the particle
            GameObject particle = Instantiate(particlePrefab, playerPosition, Quaternion.identity);

            // Gets the ParticleMover component and sets up its movement
            SpellParticleMovement mover = particle.GetComponent<SpellParticleMovement>();
            if (mover != null)
            {
                mover.Initialize(beamDirection, coneLength);
            }
            yield return new WaitForSeconds(.01f);
            
        }
    }
}
