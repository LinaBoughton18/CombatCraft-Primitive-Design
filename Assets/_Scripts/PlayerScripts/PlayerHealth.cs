using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // DATA
    public int playerHealth;
    public int maxPlayerHealth = 20;

    // UI
    public GameObject healthCanvas;
    public TMP_Text playerHealthText;

    // Start is called before the first frame update
    void Awake()
    {
        healthCanvas = GameObject.Find("HealthCanvas");
        playerHealthText = healthCanvas.GetComponentInChildren<TMP_Text>();

        // Sets starting health & updates health UI
        playerHealth = maxPlayerHealth;
        playerHealthText.text = "Health: " + playerHealth + " / " + maxPlayerHealth;
    }

    public void PlayerHit(int baseDamage)
    {
        // Edits health value
        // (in the future I can add fancy & complicated damage calculations like reducing damage based on type or something
        ChangeHealth(-baseDamage);

        // Checks for player death
        if (playerHealth <= 0)
        {
            Debug.Log("Player died D:");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            PlayerHit(2);
        }
    }

    public void ChangeHealth(int amountToChange)
    {
        //Debug.Log("playerHealth currently = " + playerHealth);
        //Debug.Log("New playerHealth = " + (playerHealth + amountToChange));
        // Edits health value
        playerHealth += amountToChange;
        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        //Debug.Log("Confirming new playerHealth: " + playerHealth);

        // Edits Health UI
        playerHealthText.text = "Health: " + playerHealth + " / " + maxPlayerHealth;
    }
}
