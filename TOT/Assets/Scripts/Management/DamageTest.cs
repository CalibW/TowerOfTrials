using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    public AttributesManager playerAtm;
    public AttributesManager enemyAtm;
    private void Update()
    {
        //Deal PLayer Damage To Enemy Health
        if(Input.GetKeyDown(KeyCode.L))
        {
            playerAtm.DealDamage(enemyAtm.gameObject);
        }

        //Dela Enemy Damage To Player Health
        if(Input.GetKeyDown(KeyCode.K))
        {
            enemyAtm.DealDamage(playerAtm.gameObject);
        }
    }
}
