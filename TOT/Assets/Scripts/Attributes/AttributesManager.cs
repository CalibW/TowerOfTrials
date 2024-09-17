using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesManager : MonoBehaviour
{
     public int health;
     public int mana;
     public int attack;
     public int defence;
     public int agility;
     public int magicPower;

   public void TakeDamage(int amount)
   {
        health -= amount;
        health = Mathf.Max(0, health);
        Vector3 randomness = new Vector3(Random.Range(0f, 0.25f), Random.Range(0f, 0.25f), Random.Range(0f, 0.25f));
        DamagePopUpGenerator.current.CreatePopUp(transform.position + randomness, amount.ToString(), Color.yellow);
   }

   public void DealDamage(GameObject target)
   {
        var atm = target.GetComponent<AttributesManager>();
        if (atm != null)
        {
            atm.TakeDamage(attack);
        }
   }
}
