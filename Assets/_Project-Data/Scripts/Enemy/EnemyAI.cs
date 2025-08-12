using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public LayerMask Ground, Player;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public int enemyHealth;
    public PlayerHealth playerHealth;

    // List of audio clips for VO
    public List <AudioClip> patrolAudioClipList;
    public List <AudioClip> chaseAudioClipList;
    public List <AudioClip> attackAudioClipList;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // Sight & Range
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake() // "Awake" triggers after the script instantiates.
    {
        player = GameObject.Find("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check if the Player is in sight and/or attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange) Patrolling(); // If the player is NOT in sight range AND attack range, then patrol.
        if (playerInSightRange && !playerInAttackRange) ChasePlayer(); // If the player IS in sight range BUT NOT in attack range, then chase the player.
        if (playerInAttackRange  && playerInSightRange) AttackPlayer(); // If the player IS in sight range AND in attack range, then attack the player.
    }

    public void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint(); // If there is no walk point set for the enemy, then search for one.

        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
        }

        // Calculate distance to the walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walk point reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
       // Calculate random point in range.
       float randomZ = Random.Range(-walkPointRange, walkPointRange);
       float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); // Search for a random point in the level / environment
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground)) // Check to see if the walk point is on the Ground and not outside of the map.
        {
            walkPointSet = true;
        }

    }

    public void ChasePlayer()
    {
        animator.SetBool("chase", true);
        navMeshAgent.SetDestination(player.position);

        // Play random Chase Audioclip
    }

    public void AttackPlayer()
    {
        // Stop the enemy moving while attacking
        navMeshAgent.SetDestination(transform.position);


        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    void playPatrolAudio()
    {
        // int x = Random.Range(0, patrolAudioClipList);
    }

    void playChaseAudio()
    {

    }

    void playAttackAudio()
    {

    }

    /* public void FlipSprite()
    {
        if (transform.rotation) >= 90f)
        {
            spriterenderer.
        }

    } */
}
