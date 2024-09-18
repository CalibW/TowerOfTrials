using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public float lookRadius;
    Transform target;
    NavMeshAgent agent;
    public Animator animator;
    public GameObject player;
    public float basicattackdistance;
    public float BossAttackDistance;
    float cooldown;

    // Add reference to the enemy's attack value
    [SerializeField] private EnemyAttributes enemyAttributes;

       void Start()
    {
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
        UpdateAgentSpeed();
        cooldown -= Time.deltaTime;
        animator.SetFloat("Speed", agent.velocity.magnitude);
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.destination = target.position;
        }

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }
        
        if((transform.position - player.transform.position).magnitude <= basicattackdistance && cooldown <= 0 && CompareTag("BasicEnemy"))
        {
            PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
            if (playerAttributes != null)
            {
                // Reduce the player's health based on the enemy's attack
                playerAttributes.TakeDamage(enemyAttributes.Strength);
            }
            cooldown = 1f;
        }

        if((transform.position - player.transform.position).magnitude <= BossAttackDistance && cooldown <= 0 && CompareTag("Boss"))
        {
            PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
            if (playerAttributes != null)
            {
                // Reduce the player's health based on the enemy's attack
                playerAttributes.TakeDamage(enemyAttributes.Strength);
            }
            cooldown = 1f;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void UpdateAgentSpeed()
    {
        if (agent != null && enemyAttributes != null)
        {
            // Adjust the speed based on agility
            agent.speed = enemyAttributes.Agility; // You might want to apply some multiplier or min/max constraints
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}



// using UnityEngine;
// using UnityEngine.AI;

// public class EnemyController : MonoBehaviour
// {
//     public float lookRadius;
//     Transform target;
//     NavMeshAgent agent;
//     public Animator animator;
//     public GameObject player;
//     public float attackdistance;
//     float cooldown;

//     [SerializeField] private EnemyAttributes enemyAttributes;

//     void Start()
//     {
//         target = PlayerManager.instance.player.transform;
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponent<Animator>();

//         if (enemyAttributes == null)
//         {
//             enemyAttributes = GetComponent<EnemyAttributes>();
//         }
//     }

//     void Update()
//     {
//         cooldown -= Time.deltaTime;
//         animator.SetFloat("Speed", agent.velocity.magnitude);
//         float distance = Vector3.Distance(target.position, transform.position);

//         if (distance <= lookRadius)
//         {
//             agent.destination = target.position;
//             enemyAttributes.SetRunSpeed(); // Run while tracking the player
//         }
//         else
//         {
//             enemyAttributes.SetWalkSpeed(); // Default to walk speed if not tracking
//         }

//         if (distance <= agent.stoppingDistance)
//         {
//             FaceTarget();
//         }

//         // if ((transform.position - player.transform.position).magnitude <= attackdistance && cooldown <= 0)
//         // {
//         //     PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
//         //     if (playerAttributes != null)
//         //     {
//         //         playerAttributes.TakeDamage(enemyAttributes.Strength);
//         //     }
//         //     cooldown = 1f;
//         // }
//     }

//     void FaceTarget()
//     {
//         Vector3 direction = (target.position - transform.position).normalized;
//         Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
//         transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
//     }

//     void OnDrawGizmosSelected()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawWireSphere(transform.position, lookRadius);
//     }
// }
