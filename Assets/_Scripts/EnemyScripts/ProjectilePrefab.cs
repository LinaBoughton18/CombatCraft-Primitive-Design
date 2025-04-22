/*---------------------------------------- BY LINA ----------------------------------------
------------------------------- CURRENTLY UNUSED IN GAME ----------------------------------

A script for an enemy projectile attack that moves a particle towards the player.

-----------------------------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePrefab : MonoBehaviour
{
    public Vector2 targetPosition;
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
