using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ImportedEnemy : MonoBehaviour
{
    public NavMeshAgent navAgent; // AI navigation agent
    public Transform player; // player's transform
    public LayerMask groundLayer, playerLayer; // Layers to detect ground and player

    public float health;
    public float walkPointRange; // Range for random patrol points
    public float timeBetweenAttacks; // Attack cooldown
    public float sightRange;
    public float attackRange;
    public int damage;

    public Animator animator; // Component for animations
    public ParticleSystem hitEffect; // Particle effect for taking damage

    private Vector3 walkPoint; // Patrol point desitination
    private bool walkPointSet; // If a walk point is set or not
    private bool alreadyAttacked; // Prevents repeated attacking
    private bool takeDamage; // Determines if the enemy should chase after being hit

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();

        // Ensures the agent works correctly on a 2D NavMesh setup
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    private void Update()
    {
        // Checks if the player is in its attack or sight range
        bool playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        bool playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        // State Control
        // If player is not in sight or attack range, patrol state
        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        // If player is in sight range, chase state
        else if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        // If player is in both sight and attack range, attack state
        else if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        // If hit & play out of sight, chase state
        else if (!playerInSightRange && takeDamage)
        {
            ChasePlayer();
        }
    }

    private void Patroling()
    {
        // If no patrol point set, find a new one
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        // Walk to patrol point
        if (walkPointSet)
        {
            navAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        //animator.SetFloat("Velocity", 0.2f);

        // Once enemy reaches patrol point, find a new patrol point
        // Search precision is 1f (in 1 unit of distance from patrol point)
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomY = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
        /*
        // Ensures walk point is on the ground????
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
        */
        walkPointSet = true;
    }

    private void ChasePlayer()
    {
        navAgent.SetDestination(player.position); // Move towards player
        //animator.SetFloat("Velocity", 0.6f);
        navAgent.isStopped = false; // Ensure the agent is not stopped
    }

    private void AttackPlayer()
    {
        navAgent.SetDestination(transform.position); // Stop moving when attacking

        if (!alreadyAttacked)
        {
            //transform.LookAt(player.position);
            alreadyAttacked = true;
            //animator.SetBool("Attack", true);
            Invoke(nameof(ResetAttack), timeBetweenAttacks); // Reset attack cooldown

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
            {
                // If attack hits the player, apply damage
                /*
                    YOU CAN USE THIS TO GET THE PLAYER HUD AND CALL THE TAKE DAMAGE FUNCTION

                PlayerHUD playerHUD = hit.transform.GetComponent<PlayerHUD>();
                if (playerHUD != null)
                {
                   playerHUD.takeDamage(damage);
                }
                 */
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        //animator.SetBool("Attack", false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage; // Reduce enemy health
        //hitEffect.Play();
        StartCoroutine(TakeDamageCoroutine()); // Trigger damage behavior

        if (health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f); // Destroy enemy if health is depleted
        }
    }

    private IEnumerator TakeDamageCoroutine()
    {
        takeDamage = true; // Mark enemy as hit to trigger chase behavior
        yield return new WaitForSeconds(2f); // Wait before resetting
        takeDamage = false;
    }

    private void DestroyEnemy()
    {
        StartCoroutine(DestroyEnemyCoroutine()); // Start destruction sequence
    }

    private IEnumerator DestroyEnemyCoroutine()
    {
        //animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.8f); // Wait before destroying object
        Destroy(gameObject); // Remove enemy from the scene
    }

    private void OnDrawGizmosSelected()
    {
        // Visual representation of attack and sight ranges in the Unity editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
