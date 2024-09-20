using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WolfEnemyAttributes : MonoBehaviour
{
    // Variables (stats) for the wolf enemy, including health, mana, and combat attributes
    public float Health;
    public float Mana;
    public float Strength;
    public float Endurance;
    public float Agility;
    public float MagicPower;
    public float ExperienceDropped;
    public static float BasicAmountKilled; // Track number of basic enemies killed
    public static float BossKilled; // Track number of bosses killed
    public ParticleSystem DeathEffect; // Particle effect for enemy death
    public float minRandom; // Minimum randomness for damage pop-ups
    public float maxRandom; // Maximum randomness for damage pop-ups
    public float walkSpeed; // Speed when the enemy is walking
    public float runSpeed; // Speed when the enemy is running
    public GameObject Enemy; // Reference to the enemy GameObject
    public NavMeshAgent agent; // NavMeshAgent for pathfinding
    [SerializeField] PlayerAttributes PlayerAtm; // Reference to the player attributes
    [SerializeField] EnemySpawning EnemySpawns; // Reference to the enemy spawning script

    void Start ()
    {
        // Get the NavMeshAgent component and update movement speed
        agent = GetComponent<NavMeshAgent>();
        UpdateMovementSpeed();
    }

    void Update()
    {
        // Check if health is less than or equal to 0
        if(Health <= 0)
        {
            // Instantiate the death effect at the enemy's position
            var DeathEffectStart = Instantiate(DeathEffect, Enemy.transform.position, Quaternion.identity);

            // Destroy the particle system after its lifetime ends
            Destroy(DeathEffectStart.gameObject, DeathEffectStart.main.startLifetime.constant);
            Destroy(gameObject); // Destroy the enemy GameObject
            PlayerAtm.Experience += ExperienceDropped; // Add experience to the player
            EnemySpawns.EnemyCount--; // Decrease the enemy count in the spawning script

            // Increment the count of enemies killed based on the enemy's tag
            if(CompareTag("BasicEnemy"))
            {
                BasicAmountKilled++;
            }
            else if(CompareTag("Boss"))
            {
                BossKilled++;
            }
        }
    }

   // Function to apply damage to the enemy and generate pop-up damage numbers
   public void TakeDamage(float amount)
   {
        Health -= amount; // Reduce health by the damage amount
        Health = Mathf.Max(0, Health); // Ensure health does not go below 0
        Vector3 randomness = new Vector3(Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom));
        DamagePopUpGenerator.current.CreatePopUp(transform.position + randomness, amount.ToString(), Color.yellow); // Create a damage pop-up
   }

   // Function to deal damage to the player
   public void DealDamage(GameObject target)
   {
        var playeratm = target.GetComponent<PlayerAttributes>(); // Get the player's attributes
        if (playeratm != null)
        {
            playeratm.TakeDamage(Strength); // Deal damage based on strength
        }
   }

   // Function to set the walking speed of the enemy
   public void SetWalkSpeed()
   {
        if (agent != null)
        {
            agent.speed = walkSpeed; // Set NavMeshAgent speed to walk speed
        }
   }

   // Function to set the running speed of the enemy
   public void SetRunSpeed()
   {
        if (agent != null)
        {
            agent.speed = runSpeed; // Set NavMeshAgent speed to run speed
        }
   }

   // Function to update the movement speed, setting a default speed if needed
   void UpdateMovementSpeed()
   {
        agent.speed = walkSpeed; // Default to walk speed
   }
}
