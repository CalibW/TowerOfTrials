using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Player;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public PlayerAttributes PlayerAttributes;
    public EnemyAttributes enemyAttributes;
    public WolfEnemyAttributes wolfEnemyAttributes;
    public bool CanAttack = true;
    public float attackdistance;
    public float AttackCooldown = 1f;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && (Player.transform.position - Enemy1.transform.position).magnitude <= attackdistance)
        {
            if(CanAttack)
            {
                SwordAttack();
                enemyAttributes.TakeDamage(PlayerAttributes.Strength);
            }
        }
        else if(Input.GetMouseButton(0) && (Player.transform.position - Enemy2.transform.position).magnitude <= attackdistance)
        {
            if(CanAttack)
            {
                SwordAttack();
                wolfEnemyAttributes.TakeDamage(PlayerAttributes.Strength);
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(CanAttack)
                {
                    SwordAttack();
                }
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
