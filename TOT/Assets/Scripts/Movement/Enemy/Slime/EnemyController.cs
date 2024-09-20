using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius; // Radius within which the enemy can detect the player
    Transform target; // Reference to the player
    NavMeshAgent agent; // NavMesh agent for enemy movement
    public Animator animator; // Animator for enemy animations
    public GameObject player; // Reference to the player GameObject
    public float basicattackdistance; // Distance for basic attack
    public float BossAttackDistance; // Distance for boss attack
    float cooldown; // Cooldown timer for attacks

    // Add reference to the enemy's attack value
    public EnemyAttributes enemyAttributes;

    void Start()
    {
        // Get the player target from PlayerManager
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Ensure enemyAttributes is set
        if (enemyAttributes == null)
        {
            enemyAttributes = GetComponent<EnemyAttributes>();
        }

        // Set the NavMeshAgent speed based on the enemy's agility
        UpdateAgentSpeed();
    }

    void Update()
    {
        // Continuously update agent speed and cooldown timer
        UpdateAgentSpeed();
        cooldown -= Time.deltaTime;
        animator.SetFloat("Speed", agent.velocity.magnitude); // Set animation speed based on movement

        float distance = Vector3.Distance(target.position, transform.position); // Calculate distance to the player
        if (distance <= lookRadius) // If the player is within the look radius
        {
            agent.destination = target.position; // Move towards the player
        }

        if (distance <= agent.stoppingDistance) // If close enough to the player
        {
            FaceTarget(); // Face the player
        }
        
        // Basic attack logic for regular enemies
        if ((transform.position - player.transform.position).magnitude <= basicattackdistance && cooldown <= 0 && CompareTag("BasicEnemy"))
        {
            PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
            if (playerAttributes != null)
            {
                // Reduce the player's health based on the enemy's attack
                playerAttributes.TakeDamage(enemyAttributes.Strength);
            }
            cooldown = 1f; // Reset cooldown
        }

        // Attack logic for bosses
        if ((transform.position - player.transform.position).magnitude <= BossAttackDistance && cooldown <= 0 && CompareTag("Boss"))
        {
            PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
            if (playerAttributes != null)
            {
                // Reduce the player's health based on the enemy's attack
                playerAttributes.TakeDamage(enemyAttributes.Strength);
            }
            cooldown = 1f; // Reset cooldown
        }
    }

    void FaceTarget()
    {
        // Calculate direction to the target and rotate to face it
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void UpdateAgentSpeed()
    {
        // Adjust the speed of the NavMesh agent based on the enemy's agility
        if (agent != null && enemyAttributes != null)
        {
            agent.speed = enemyAttributes.Agility; // You might want to apply some multiplier or min/max constraints
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize the look radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
