using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrikingDistanceCheck : MonoBehaviour
{
    // References to the player and enemy, assigned in awake
    public GameObject playerTarget { get; set; }
    private EnemyBase enemy;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");

        enemy = GetComponentInParent<EnemyBase>();
    }

    // Sets aggro bool to true when player enters the trigger
    // & false when player exits the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == playerTarget)
        {
            enemy.SetStrikingDistanceBool(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerTarget)
        {
            enemy.SetStrikingDistanceBool(false);
        }
    }
}
