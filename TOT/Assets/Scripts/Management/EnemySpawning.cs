using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;
    public float EnemyCount;
    public float MaxEnemy;
    public float MaxEnemyAllowed;
    public float RemainingSpawns;
    public float SpawnRate;
    public float InnerRadius;  // Minimum distance from the center (inner boundary)
    public float OuterRadius; // Maximum distance from the center (outer boundary)
    public int MaxAttempts = 10;     // Number of attempts to find a valid position on NavMesh

    void Start()
    {
        RemainingSpawns = MaxEnemyAllowed;
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while(RemainingSpawns > 0)
        {
            if (MaxEnemy <= MaxEnemyAllowed)
            {
                if (EnemyCount < MaxEnemy)
                {
                    Vector3 spawnPosition;
                    if (TryGetRandomNavMeshPositionInRing(out spawnPosition))
                    {
                        GameObject enemyToSpawn = Random.value > 0.5f ? Enemy1 : Enemy2;
                        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
                        EnemyCount++;
                        RemainingSpawns--;
                    }
                }
                
                yield return new WaitForSeconds(SpawnRate);
            }
        }
    }

    // Generate a random position within a ring (between InnerRadius and OuterRadius)
    bool TryGetRandomNavMeshPositionInRing(out Vector3 result)
    {
        for (int i = 0; i < MaxAttempts; i++)
        {
            Vector3 randomPoint = GetRandomPointInRing(InnerRadius, OuterRadius);
            NavMeshHit hit;

            // Check if the random point is on the NavMesh
            if (NavMesh.SamplePosition(randomPoint, out hit, OuterRadius, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    // Function to generate a random point within a ring (between two radii)
    Vector3 GetRandomPointInRing(float innerRadius, float outerRadius)
    {
        // Generate a random angle
        float angle = Random.Range(0f, Mathf.PI * 2);

        // Generate a random distance between the inner and outer radius
        float distance = Mathf.Sqrt(Random.Range(innerRadius * innerRadius, outerRadius * outerRadius));

        // Calculate the point's x and z positions using the angle and distance
        float x = Mathf.Cos(angle) * distance;
        float z = Mathf.Sin(angle) * distance;

        // Return the point, relative to the spawner's position (assumed as the center)
        return new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
    }

    // Visualize the ring-shaped spawn area using Gizmos
    void OnDrawGizmos()
    {
        // Draw outer boundary (135m radius)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, OuterRadius);

        // Draw inner boundary (90m radius)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, InnerRadius);
    }
}