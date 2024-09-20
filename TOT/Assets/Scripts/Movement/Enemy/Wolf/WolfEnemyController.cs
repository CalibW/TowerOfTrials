using UnityEngine;
using UnityEngine.AI;

public class WolfEnemyController : MonoBehaviour
{
    public float lookRadius;
    Transform target;
    NavMeshAgent agent;
    public Animator animator;
    public GameObject player;
    public float attackdistance;
    float cooldown;

    public WolfEnemyAttributes wolfenemyAttributes;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (wolfenemyAttributes == null)
        {
            wolfenemyAttributes = GetComponent<WolfEnemyAttributes>();
        }
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        animator.SetFloat("Speed", agent.velocity.magnitude);
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.destination = target.position;
            wolfenemyAttributes.SetRunSpeed(); // Run while tracking the player
        }
        else
        {
            wolfenemyAttributes.SetWalkSpeed(); // Default to walk speed if not tracking
        }

        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }

        if ((transform.position - player.transform.position).magnitude <= attackdistance && cooldown <= 0)
        {
            PlayerAttributes playerAttributes = player.GetComponent<PlayerAttributes>();
            if (playerAttributes != null)
            {
                playerAttributes.TakeDamage(wolfenemyAttributes.Strength);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}