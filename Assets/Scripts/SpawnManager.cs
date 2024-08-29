using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;

    public GameObject forage1;
    public GameObject forage2;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 3f);
        InvokeRepeating("SpawnForage", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        Instantiate(enemy, randomPosition, enemy.transform.rotation);
    }
    
    void SpawnForage()
    {
        Vector2 randomPosition = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
        Instantiate(forage1, randomPosition, forage1.transform.rotation);

        randomPosition = new Vector2(Random.Range(-6, 6), Random.Range(-6, 6));
        Instantiate(forage2, randomPosition, forage2.transform.rotation);
    }
}