using UnityEngine;
using UnityEngine.AI;

public class WolfEnemyController : MonoBehaviour
{
    public float lookRadius; // Radius within which the enemy can detect the player
    Transform target; // Target reference for the player
    NavMeshAgent agent; // NavMeshAgent component for enemy movement
    public Animator animator; // Animator component for controlling animations
    public GameObject player; // Reference to the player GameObject
    public float attackdistance; // Distance within which the enemy can attack the player
    float cooldown; // Cooldown timer for attacks

    [SerializeField] private WolfEnemyAttributes wolfenemyAttributes; // Reference to the wolf enemy's attributes

    void Start()
    {
        // Get the player's transform from the PlayerManager instance
        target = PlayerManager.instance.player.transform; 
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        animator = GetComponent<Animator>(); // Get the Animator component

        // Check if wolfenemyAttributes is assigned; if not, get it from the GameObject
        if (wolfenemyAttributes == null)
        {
            wolfenemyAttributes = GetComponent<WolfEnemyAttributes>();
        }
    }

    void Update()
    {
        cooldown -= Time.deltaTime; // Decrease cooldown over time
        animator.SetFloat("Speed", agent.velocity.magnitude); // Update the animator with the enemy's speed
        float distance = Vector3.Distance(target.position, transform.position); // Calculate distance to the player

        // If the player is within the look radius, move towards them
        if (distance <= lookRadius)
        {
            agent.destination = target.position; // Set the agent's destination to the player's position
            wolfenemyAttributes.SetRunSpeed(); // Run while tracking the player
        }
        else
        {
            wolfenemyAttributes.SetWalkSpeed(); // Default to walk speed if not tracking
        }

        // If within stopping distance, face the target
        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }

        // If within attack distance and cooldown is over, attack the player
        if ((transform.position - player.transform.position).magnitude <= attackdistance && cooldown <= 0)
        {
            PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>(); // Get the player's attributes
            if (playerAttributes != null) // Check if the player exists
            {
                playerAttributes.TakeDamage(wolfenemyAttributes.Strength); // Deal damage to the player
            }
            cooldown = 1f; // Reset cooldown
        }
    }

    // Function to rotate the enemy to face the target
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; // Get direction to the target
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Create a rotation to face the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Smoothly rotate towards the target
    }

    // Function to visualize the look radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set gizmo color to red
        Gizmos.DrawWireSphere(transform.position, lookRadius); // Draw a wire sphere to represent the look radius
    }
}
