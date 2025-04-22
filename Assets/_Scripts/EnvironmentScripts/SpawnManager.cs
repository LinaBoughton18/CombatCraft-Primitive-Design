//---------------------------------------- BY LINA ----------------------------------------//
//-----------------------------------------------------------------------------------------//

// This script spawns items into the scene, currently items and enemies.

// For both, it references their base prefabs (itemPrefab & enemyPrefab, repsectively)
// for basic behaviors such as the ability to pathfind or the ability to be picked up by the player.
// To assign unique attributes (such as sprites), it references a database of scriptable objects
// (either itemSODatabase or enemySODatabase) and combines it with the base prefab.

// When the game starts, it currently spawns in 10 of each of the available items in the game

//-----------------------------------------------------------------------------------------//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The prefab for a physical item
    public GameObject itemPrefab; // Set in the Unity editor
    // Database of itemSOs
    private ItemSODatabase itemSODatabase;

    // The prefab for a physical enemy
    public GameObject enemyPrefab; // Set in the Unity editor -- Currently AStarEnemy, shall be changed in future versions
    private EnemySODatabase enemySODatabase;
    
    void Awake()
    {
        itemSODatabase = GetComponent<ItemSODatabase>();
        enemySODatabase = GetComponent<EnemySODatabase>();
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnItem("Burberry", new Vector3(-4.31f, -4.32f, 0));
            SpawnItem("Popping Seeds", new Vector3(-2.7f, -4.32f, 0));
            SpawnItem("Neptune Salt", new Vector3(-1.4f, -4.32f, 0));
            SpawnItem("Foxtail Leaf", new Vector3(0f, -4.32f, 0));
        }
    }

    
    void SpawnEnemy(string enemyName, Vector3 position)
    {
        // Finds the corresponding enemySO
        EnemySO enemySO = enemySODatabase.GetEnemySOByName(enemyName);

        // Spawns in the enemy
        GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);

        // Assigns the SO information
        newEnemy.GetComponent<EnemyBase2>().PassEnemySOData(enemySO);
    }
    

    public void SpawnItem(string itemName, Vector3 position)
    {
        // Finds the corresponding ItemSO
        ItemSO itemSO = itemSODatabase.GetItemSOByName(itemName);

        // Spawns in the item
        GameObject newItem = Instantiate(itemPrefab, position, Quaternion.identity);

        // Assigns the SO information
        newItem.GetComponent<Item>().PassItemSOData(itemSO);
    }
}