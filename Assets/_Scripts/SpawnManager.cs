using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The prefab for a physical item
    public GameObject itemPrefab; // Set in the Unity editor

    private ItemSODatabase itemSODatabase;
    
    void Awake()
    {
        itemSODatabase = GetComponent<ItemSODatabase>();
    }

    private void Start()
    {
        SpawnItem("Burberry", new Vector3(-4.31f, -4.32f, 0));
        SpawnItem("Popping Seeds", new Vector3(-2.7f, -4.32f, 0));
        SpawnItem("Neptune Salt", new Vector3(-1.4f, -4.32f, 0));
        SpawnItem("Foxtail Leaf", new Vector3(0f, -4.32f, 0));
    }

    /*
    void SpawnEnemy()
    {
        //Vector2 randomPosition = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        //Instantiate(enemy, randomPosition, enemy.transform.rotation);

        //Vector2 spawnPosition = new Vector2(6, 6);
        //Instantiate(enemy, spawnPosition, enemy.transform.rotation);
    }
    */

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