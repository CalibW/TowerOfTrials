using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyAttributes : MonoBehaviour
{
    public float Health;
    public float Mana;
    public float Strength;
    public float Endurance;
    public float Agility;
    public float MagicPower;
    public float ExperienceDropped;
    public float AmountKilled;
    public ParticleSystem DeathEffect;
    public float minRandom;
    public float maxRandom;
    public GameObject Enemy;
    [SerializeField] PlayerAttributes PlayerAtm;
    [SerializeField] EnemySpawning EnemySpawns;

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
          AmountKilled++;
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
