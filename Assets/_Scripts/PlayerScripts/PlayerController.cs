using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

// This class is for controlling player MOVEMENT!!!!!!!!!!

public class PlayerController : MonoBehaviour
{
    // Movement Data
    private Rigidbody2D playerRigidbody;
    private Vector2 playerInput;
    public float moveSpeed = 5f;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // We do movement in fixedupdate because it's not dependant on frames, but time
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Grabs player input & normalizes it (so diagonals aren't faster)
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 playerInputNormalized = playerInput.normalized;
        // Moves the player in the correct direction
        playerRigidbody.velocity = playerInputNormalized * moveSpeed;
    }
}