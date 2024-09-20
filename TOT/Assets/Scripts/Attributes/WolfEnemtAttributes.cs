using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WolfEnemyAttributes : MonoBehaviour
{
    public float Health;
    public float Mana;
    public float Strength;
    public float Endurance;
    public float Agility;
    public float MagicPower;
    public float ExperienceDropped;
    public static float BasicAmountKilled;
    public static float BossKilled;
    public ParticleSystem DeathEffect;
    public float minRandom;
    public float maxRandom;
    public float walkSpeed;
    public float runSpeed;
    public GameObject Enemy;
    public NavMeshAgent agent;
    public PlayerAttributes PlayerAtm;
    public EnemySpawning EnemySpawns;

    void Start ()
    {
     agent = GetComponent<NavMeshAgent>();
     UpdateMovementSpeed();

    }
    void Update()
    {
     if(Health <= 0)
     {
          // Instantiate the death effect
          var DeathEffectStart = Instantiate(DeathEffect, Enemy.transform.position, Quaternion.identity);

          // Destroy the particle system after its lifetime ends
          Destroy(DeathEffectStart.gameObject, DeathEffectStart.main.startLifetime.constant);
          Destroy(gameObject);
          PlayerAtm.Experience += ExperienceDropped;
          EnemySpawns.EnemyCount--;

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

   public void TakeDamage(float amount)
   {
        Health -= amount;
        Health = Mathf.Max(0, Health);
        Vector3 randomness = new Vector3(Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom));
        DamagePopUpGenerator.current.CreatePopUp(transform.position + randomness, amount.ToString(), Color.yellow);
   }

   public void DealDamage(GameObject target)
   {
        var playeratm = target.GetComponent<PlayerAttributes>();
        if (playeratm != null)
        {
            playeratm.TakeDamage(Strength);
        }
   }

   public void SetWalkSpeed()
    {
        if (agent != null)
        {
            agent.speed = walkSpeed;
        }
    }

    public void SetRunSpeed()
    {
        if (agent != null)
        {
            agent.speed = runSpeed;
        }
    }

    void UpdateMovementSpeed()
    {
        // Optional: You can set a default speed here if needed
        agent.speed = walkSpeed; // Default to walk speed
    }
}