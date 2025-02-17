using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// THIS IS THE OLD BASIC ENEMY SCRIPT
// THIS IS GOING TO BE IMPROVED AS OF NOW

public class OldEnemy : MonoBehaviour
{
    // Movement
    private Rigidbody2D enemyRigidbody;
    public GameObject player;
    public Rigidbody2D playerRigidbody;
    public PlayerController playerController;

    // Speed
    public float speed;

    // Stats
    [SerializeField]
    private int enemyBaseDamage;

    // Temporary Guides
    public GameObject targetPositionDot;
    public bool movementStyle;
    
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (movementStyle)
        {
            MoveToPlayer();
        }
        else
        {
            MoveDirectToPlayer();
        }
    }

    public Vector2 closingVelocity;
    public Vector2 relativeDistance;
    public float closingTime;
    public Vector2 targetPosition;
    public Vector2 targetDirection;

    // A basic tracking & movement algorithm
    // If player is standing still, move directly to them
    // If player is moving (velocity != 0), move to intercept them
    private void MoveToPlayer()
    {
        // If player is standing still, move directly to player
        if (playerRigidbody.velocity == Vector2.zero)
        {
            targetPosition = player.transform.position;
            targetPositionDot.transform.position = targetPosition;
            targetDirection = targetPosition - new Vector2(transform.position.x, transform.position.y);
            targetDirection.Normalize();

            enemyRigidbody.velocity = targetDirection * speed;
        }
        // If player is moving, move to intercept them
        else
        {
            // Calculating an interception point for the enemy to travel to
            // Assinging variables
            closingVelocity = playerRigidbody.velocity - enemyRigidbody.velocity;
            relativeDistance = player.transform.position - transform.position;
            closingTime = relativeDistance.magnitude / closingVelocity.magnitude;

            // Calculates & assigns target position
            targetPosition = player.transform.position
                + new Vector3(playerRigidbody.velocity.x * closingTime,
                playerRigidbody.velocity.y * closingTime, 0);
            //targetPosition = player.transform.position 
                //+ new Vector3(enemyRigidbody.velocity.x * closingTime,
                //enemyRigidbody.velocity.y * closingTime, 0);
            targetPositionDot.transform.position = targetPosition;

            // Moves enemy to target position
            targetDirection = targetPosition - new Vector2(transform.position.x, transform.position.y);
            targetDirection.Normalize();
            enemyRigidbody.velocity = targetDirection * speed;
        }
    }

    private void MoveDirectToPlayer()
    {
        targetPosition = player.transform.position;
        targetPositionDot.transform.position = targetPosition;
        targetDirection = targetPosition - new Vector2(transform.position.x, transform.position.y);
        targetDirection.Normalize();

        enemyRigidbody.velocity = targetDirection * speed;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Spell Effect"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            playerController.PlayerHit(enemyBaseDamage);
            Destroy(gameObject);
        }
    }
    */
}