using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forage : MonoBehaviour
{
    private SpellController spellControllerScript;

    public int forageID;

    // Start is called before the first frame update
    void Start()
    {
        spellControllerScript = GameObject.Find("Player").GetComponent<SpellController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            spellControllerScript.inventory[forageID]++;
            Destroy(gameObject);
        }
    }
}
