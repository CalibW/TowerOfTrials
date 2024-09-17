using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public float Level;
    public float Experience;
    public float FirstLevelExperience;
    public float ExperienceToNextLevel;
    public float ExpIncrease;
    public float Health;
    public float MaxHealth;
    public float Mana;
    public float MaxMana;
    public float Strength;
    public float Endurance;
    public float Agility;
    public float MagicPower;
    public float StatPointsGiven;
    public float minRandom;
    public float maxRandom;
    [SerializeField] StatsMenu StatMenu;

    void Start()
    {
     MaxMana = Mana;
     MaxHealth = Health;
     ExperienceToNextLevel = FirstLevelExperience;
    }

    void Update()
    {
      if(Level == 0)
      {
         ExperienceToNextLevel = FirstLevelExperience;
      } else if (Level > 0 && Level < 25)
      {
         ExperienceToNextLevel = FirstLevelExperience * Mathf.Pow(ExpIncrease, Level);
      }
      else if(Level == 25)
      {
         ExperienceToNextLevel = 9999999;
      }
      LevelUp();
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
        var enemyatm = target.GetComponent<EnemyAttributes>();
        if (enemyatm != null)
        {
          enemyatm.TakeDamage(Strength);
        }
     }

     public void LevelUp()
     {
         if(Experience >= ExperienceToNextLevel)
      {
         Experience = Experience - ExperienceToNextLevel;
         Level += 1;
         StatMenu.StatPoints += StatPointsGiven;
         Health = MaxHealth;
         Mana = MaxMana;
      }
     }
}
