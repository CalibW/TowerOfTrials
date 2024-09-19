using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
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
    public GameObject Enemy;
    public PlayerAttributes PlayerAtm;
    public EnemySpawning EnemySpawns;

    void Update()
    {
     if(Health <= 0)
     {
          if(CompareTag("BasicEnemy"))
          {
               // Instantiate the death effect
               var DeathEffectStart = Instantiate(DeathEffect, Enemy.transform.position, Quaternion.identity);
               // Destroy the particle system after its lifetime ends
               Destroy(DeathEffectStart.gameObject, DeathEffectStart.main.startLifetime.constant);
               Destroy(gameObject);
               PlayerAtm.Experience += ExperienceDropped;
               EnemySpawns.EnemyCount--;
               BasicAmountKilled++;
          }
          else if(CompareTag("Boss"))
          {
               // Instantiate the death effect
               var DeathEffectStart = Instantiate(DeathEffect, Enemy.transform.position, Quaternion.identity);
               // Destroy the particle system after its lifetime ends
               Destroy(DeathEffectStart.gameObject, DeathEffectStart.main.startLifetime.constant);
               Destroy(gameObject);
               PlayerAtm.Experience += ExperienceDropped;
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
}
