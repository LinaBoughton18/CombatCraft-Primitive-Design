using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyDamage : MonoBehaviour
{
    // Movement
    public GameObject player;
    public PlayerController playerController;

    // Stats
    [SerializeField]
    private int enemyBaseDamage;

    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Spell Effect"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            playerController.PlayerHit(enemyBaseDamage);
            Destroy(gameObject);
        }
    }
}