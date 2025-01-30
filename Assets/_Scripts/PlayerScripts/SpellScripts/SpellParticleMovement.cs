using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellParticleMovement : MonoBehaviour
{
    // Moves a particle in a direction for for a certain distance

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
        // Move the particle
        transform.position += moveDirection * Time.deltaTime;

        // Despawn the particle if it exceeds the max distance
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}