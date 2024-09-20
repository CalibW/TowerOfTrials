using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Player;
    public List<GameObject> Enemy1;
    public PlayerAttributes PlayerAttributes;
    public bool CanAttack = true;
    public float attackdistance;
    public float AttackCooldown = 1f;

    void Update()
    {
        for(int i = 0; i < Enemy1.Count; i++)
        {
            if(Input.GetMouseButtonDown(0) && ( Enemy1[i].transform.position- Player.transform.position).magnitude <= attackdistance)
            {
            if(CanAttack)
            {
                SwordAttack();
                if(Enemy1[i].gameObject.GetComponent<EnemyAttributes>() != null)
                {
                    if(Enemy1[i].gameObject.GetComponent<EnemyAttributes>().Health <= 0)
                    {
                      Enemy1.Remove(Enemy1[i]);
                    }
                    Enemy1[i].gameObject.GetComponent<EnemyAttributes>().TakeDamage(PlayerAttributes.Strength);
              
                }
                if(Enemy1[i].gameObject.GetComponent<WolfEnemyAttributes>() != null)
                {
                      if(Enemy1[i].gameObject.GetComponent<WolfEnemyAttributes>().Health <= 0)
                    {
                      Enemy1.Remove(Enemy1[i]);
                    }
                    Enemy1[i].gameObject.GetComponent<WolfEnemyAttributes>().TakeDamage(PlayerAttributes.Strength);
                
                }

            }
            }
        }
            if(Input.GetMouseButtonDown(0))
            {
                if(CanAttack)
                {
                    SwordAttack();
                }
            }
        
    }

    public void SwordAttack()
    {
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
       CanAttack = true; 

    }
}
    