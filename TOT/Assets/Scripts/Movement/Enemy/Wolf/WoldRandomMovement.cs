using UnityEngine;
using UnityEngine.AI;

public class WolfRandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public float range; // radius of sphere
    public Transform centrePoint; // centre of the area the agent wants to move around in

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        centrePoint = gameObject.transform;

        WolfEnemyAttributes enemyAttributes = GetComponent<WolfEnemyAttributes>();
        if (enemyAttributes != null)
        {
            agent.speed = enemyAttributes.walkSpeed; // Set speed to walk speed
        }
    }

    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}