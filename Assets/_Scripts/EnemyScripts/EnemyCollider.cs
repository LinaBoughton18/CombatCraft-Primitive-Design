using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyCollider : MonoBehaviour
{
    // Movement
    public GameObject player;
    public PlayerController playerController;
    public PlayerHealth playerHealth;

    // Parent
    private GameObject parent;
    private EnemyBase2 parentBaseScript;
    private List<ItemTuple> itemsDropped;

    private SpawnManager spawnManager;

    // Stats
    [SerializeField]
    private int enemyBaseDamage;

    private void Awake()
    {
        // Find/Assign parent object
        parent = transform.parent.gameObject;
        parentBaseScript = parent.GetComponent<EnemyBase2>();
        itemsDropped = parentBaseScript.enemySO.itemsDropped;

        // Find/Assign SpawnManager
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        // Find/Assign player object
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerHealth = player.GetComponentInChildren<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spell Effect"))
        {
            Destroy(collision.gameObject);
            Destroy(parent);
        }

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit Player for " + enemyBaseDamage + " health");
            playerHealth.PlayerHit(enemyBaseDamage);
            Die();
        }
    }

    private void Die()
    {
        // Obtain position
        Vector3 position = parent.transform.position;

        // Loop through items dropped, drop the appropriate amount of items
        foreach (ItemTuple i in itemsDropped)
        {
            for (int j = 0; j < i.quantity; j++)
            {
                spawnManager.SpawnItem(i.item.itemName, position);
            }
        }

        Destroy(parent);
    }
}