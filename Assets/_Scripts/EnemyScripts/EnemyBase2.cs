using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase2 : MonoBehaviour
{
    // TODO: Add finite state machine (Idle, Chase, Attack)
    // 
    
    // ENEMY DATA -------------------------------------------------//
    public EnemySO enemySO; // The SO we pull our data from
    public int currentHealth; // The enemy's CURRENT health

    #region //------COMPONENTS AND SCENE OBJECTS------//
    // Scene Objects
    private GameObject player;

    // Enemy Components & Child Objects
    [SerializeField] private GameObject spriteObject; // Assigned in Unity editor
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    // From Imported Enemy:
    public NavMeshAgent navAgent; // AI navigation agent
    public LayerMask groundLayer, playerLayer; // Layers to detect ground and player

    public float walkPointRange; // Range for random patrol points
    public float timeBetweenAttacks; // Attack cooldown
    public float sightRange;
    public float attackRange;

    private Vector3 walkPoint; // Patrol point desitination
    private bool walkPointSet; // If a walk point is set or not
    private bool alreadyAttacked; // Prevents repeated attacking
    private bool takeDamage; // Determines if the enemy should chase after being hit


    #endregion -------------------------------------------------//

    #region ENEMY SETUP FUNCTIONS ------------------------------------------//

    private void Awake()
    {
        // Set player object
        player = GameObject.Find("Player");

        // Set components
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        navAgent = GetComponent<NavMeshAgent>();

        // Ensures the agent works correctly on a 2D NavMesh setup
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    public void PassEnemySOData(EnemySO enemySO)
    {
        // Passes along the proper itemSO
        this.enemySO = enemySO;

        // Sets the sprite & proper size
        spriteRenderer.sprite = enemySO.enemySprite;
        FitSpriteToCollider();

        // Set currentHealth to maxHealth
        currentHealth = enemySO.maxHealth;
    }

    // Alters the size of the sprite to fit within the borders of the collider
    private void FitSpriteToCollider()
    {
        // Get the size of the sprite & box collider
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size; // in world unity
        Vector2 colliderSize = boxCollider.size;

        // Calculate scale needed to fit sprite within collider
        float scaleX = colliderSize.x / spriteSize.x;
        float scaleY = colliderSize.y / spriteSize.y;

        // Applies scale to the sprite while maintaining the aspect ratio
        float scale = Mathf.Min(scaleX, scaleY);
        spriteObject.transform.localScale = new Vector3(scale, scale, 1f);
    }

    #endregion -------------------------------------------------//

    private void Update()
    {
        // Checks if player is in attack or sight range
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        // State Control
        // If player is not in sight or attack range, patrol state
        if (!playerInSightRange && !playerInAttackRange)
        {
            IdleState();
        }
        // If player is in sight range, chase state
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChaseState();
        }
        // If player is in both sight and attack range, attack state
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackState();
        }
        // If hit & play out of sight, chase state
        else if (!playerInSightRange && takeDamage)
        {
            ChaseState();
        }
    }

    private void IdleState()
    {
        //Debug.Log("Hello from IdleState");
    }

    private void ChaseState()
    {
        Debug.Log("Hello from ChaseState");
    }

    private void AttackState()
    {
        Debug.Log("Hello from AttackState");
    }




    // Controls the visuals for the gizmos
    private void OnDrawGizmosSelected()
    {
        // Visual representation of attack and sight ranges in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}


// OLD VERSION THAT USES A* PATHFINDING
// TO SET THIS UP PROPERLY: ADD THE SCRIPT COMPONENTS: Seeker, AIDestinationSetter, & AIPath (2D, 3D)
/*
public class EnemyBase2 : MonoBehaviour
{
    // ENEMY DATA -------------------------------------------------//
    public EnemySO enemySO; // The SO we pull our data from
    public int currentHealth; // The enemy's CURRENT health

    #region //------COMPONENTS AND SCENE OBJECTS------//
    // Scene Objects
    private GameObject player;

    // Enemy Components & Child Objects
    [SerializeField] private GameObject spriteObject; // Assigned in Unity editor
    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider;

    private AIDestinationSetter destinationSetter;

    #endregion -------------------------------------------------//

    #region ENEMY SETUP ------------------------------------------//
    private void Awake()
    {
        // Set scene objects & components
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");

        // Set player as destination
        destinationSetter = GetComponent<AIDestinationSetter>();
        destinationSetter.target = player.transform;

        // Set currentHealth to maxHealth
        currentHealth = enemySO.maxHealth;
    }

    public void PassEnemySOData(EnemySO enemySO)
    {
        // Passes along the proper itemSO
        this.enemySO = enemySO;

        // Sets the sprite & proper size
        spriteRenderer.sprite = enemySO.enemySprite;
        FitSpriteToCollider();
    }

    // Alters the size of the sprite to fit within the borders of the collider
    private void FitSpriteToCollider()
    {
        // Get the size of the sprite & box collider
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size; // in world unity
        Vector2 colliderSize = boxCollider.size;

        // Calculate scale needed to fit sprite within collider
        float scaleX = colliderSize.x / spriteSize.x;
        float scaleY = colliderSize.y / spriteSize.y;

        // Applies scale to the sprite while maintaining the aspect ratio
        float scale = Mathf.Min(scaleX, scaleY);
        spriteObject.transform.localScale = new Vector3(scale, scale, 1f);
    }

    #endregion -------------------------------------------------//
}
*/