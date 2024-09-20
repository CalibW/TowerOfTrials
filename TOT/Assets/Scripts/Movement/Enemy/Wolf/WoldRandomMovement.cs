using UnityEngine;
using UnityEngine.AI; // Importing NavMesh for AI navigation

public class WolfRandomMovement : MonoBehaviour
{
    public NavMeshAgent agent; // Reference to the NavMesh agent for movement
    public Animator animator; // Reference to the animator for handling animations
    public float range; // Radius of the sphere within which the agent will move
    public Transform centrePoint; // Center point of the area the agent wants to move around

    void Start()
    {
        // Get the necessary components
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        animator = GetComponent<Animator>(); // Get the Animator component
        centrePoint = gameObject.transform; // Set the centre point to the agent's position

        // Get WolfEnemyAttributes component to set the agent's speed
        WolfEnemyAttributes enemyAttributes = GetComponent<WolfEnemyAttributes>();
        if (enemyAttributes != null)
        {
            agent.speed = enemyAttributes.walkSpeed; // Set the agent's speed to the enemy's walk speed
        }
    }

    void Update()
    {
        // Check if the agent has completed its path
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point; // Variable to store the random point
            // Try to find a random point within the specified range
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); // Visualize the point with a ray
                agent.SetDestination(point); // Set the agent's destination to the random point
            }
            animator.SetFloat("Speed", agent.velocity.magnitude); // Update animator speed parameter
        }
    }

    // Method to find a random point within a specified range around a center point
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        // Generate a random point within a sphere
        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit; // Store the hit information from the NavMesh

        // Sample the NavMesh to see if the random point is valid
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position; // If valid, set the result to the hit position
            return true; // Return true to indicate success
        }

        result = Vector3.zero; // If not valid, return zero vector
        return false; // Return false to indicate failure
    }

    // Visualize the movement range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set gizmo color to red
        Gizmos.DrawWireSphere(transform.position, range); // Draw a wire sphere to represent the range
    }
}
