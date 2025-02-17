using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySODatabase : MonoBehaviour
{
    public List<EnemySO> enemySOList; // Manually add the EnemySOs to the list in Unity
    public Dictionary<string, EnemySO> enemySODictionary;

    void Awake()
    {
        CreateEnemySODictionary();
    }

    private void CreateEnemySODictionary()
    {
        // Creates a dictionary
        enemySODictionary = new Dictionary<string, EnemySO>();

        // Runs through the list of itemSOs,
        // creating a new dictionary entry for each
        // itemName is used as the key (not name, but itemName!)
        foreach (EnemySO enemy in enemySOList)
        {
            enemySODictionary[enemy.enemyName] = enemy;
        }
    }


    // A retrieval method for grabbing this item from somewhere else
    public EnemySO GetEnemySOByName(string enemyName)
    {
        if (enemySODictionary.TryGetValue(enemyName, out var obj))
        {
            return obj;
        }
        // Just in case there's a missing entry for whatever
        Debug.Log("ItemSO location for " + enemyName + " is null");
        return null;
    }

}