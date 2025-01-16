using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement Data
    private Rigidbody2D playerRigidbody;
    private Vector2 playerInput;
    public float moveSpeed = 5f;

    // Health Data
    public int playerHealth;
    public TMP_Text playerHealthText;
    public int maxPlayerHealth = 20;

    //=====FUNCTIONALITY=====//

    private void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {

    }

    // We do movement in fixedupdate because it's not dependant on frames, but time
    private void FixedUpdate() {
        MovePlayer();
    }

    //=====MOVEMENT=====//
    private void MovePlayer() {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 playerInputNormalized = playerInput.normalized;
        playerRigidbody.velocity = playerInputNormalized * moveSpeed;
    }

    //=====HEALTH=====//
    public void PlayerHit(int baseDamage)
    {
        // Edits health value
        // (in the future I can add fancy & complicated damage calculations like reducing damage based on type or something
        ChangeHealth(baseDamage);

        // Checks for player death
        if (playerHealth <= 0)
        {
            Debug.Log("Player died :(");
        }
    }

    public void ChangeHealth(int amountToChange)
    {
        // Edits health value
        playerHealth += amountToChange;
        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        // Edits Health UI
        playerHealthText.text = "Health: " + playerHealth + " / " + maxPlayerHealth;
    }
}