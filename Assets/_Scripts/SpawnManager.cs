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
    public GameObject enemyPrefab; // Set in the Unity editor
    private EnemySODatabase enemySODatabase;
    
    void Awake()
    {
        itemSODatabase = GetComponent<ItemSODatabase>();
        enemySODatabase = GetComponent<EnemySODatabase>();
    }

    private void Start()
    {
        SpawnItem("Burberry", new Vector3(-4.31f, -4.32f, 0));
        SpawnItem("Popping Seeds", new Vector3(-2.7f, -4.32f, 0));
        SpawnItem("Neptune Salt", new Vector3(-1.4f, -4.32f, 0));
        SpawnItem("Foxtail Leaf", new Vector3(0f, -4.32f, 0));

        //SpawnEnemy("Circle", new Vector3(9, 2, 0));
        //SpawnEnemy("Circle", new Vector3(8, -7, 0));
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