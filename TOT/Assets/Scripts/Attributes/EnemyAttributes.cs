using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
     //creating varaibles(stats) for the enemies as well as linking the effects and scripts to this script
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
     //check if health is less than or equal to 0
     if(Health <= 0)
     {
          //if the tag of the gameobject is BasicEnemy then instntiate the death effect at the Enemies location and destroy the Enemy and death effect, as well as give experience to player and decrease the amount of spawned enemies in the enemy spawning script, as well as increase the amount of Basic Enemies Killed
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
          //if the tag of the gameobject is Boss then instantiate the death effect and destroy the deatheffect and the boss as well as give experience to the player and increase the amount of boss killed.
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

   //function to apply damage to the enemy and generate pop up damage damage numbers
   public void TakeDamage(float amount)
   {
        Health -= amount;
        Health = Mathf.Max(0, Health);
        Vector3 randomness = new Vector3(Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom));
        DamagePopUpGenerator.current.CreatePopUp(transform.position + randomness, amount.ToString(), Color.yellow);
   }

   //function to deal damage to player
   public void DealDamage(GameObject target)
   {
        var playeratm = target.GetComponent<PlayerAttributes>();
        if (playeratm != null)
        {
            playeratm.TakeDamage(Strength);
        }
   }
}
