using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //creating varaibles, and linking game objeects to the script, as well as linking other scripts to this script
    public GameObject impactVFX;
    public float impactidie;
    public float lifeline;
    public bool collided;
    private PlayerAttributes playeratm;

    void Start()
    {
        // Find the player's AttributesManager in the scene when game strts
        playeratm = FindObjectOfType<PlayerAttributes>();
    }

    void OnCollisionEnter(Collision co)
    {
        //if the projectile collides with a game object whose tag is BasicEnemy or Boss, then apply the damage to said enemy or boss and play the impact effect and destroy it after the impactidie as well as destroy the projectile
        if (co.gameObject.CompareTag("BasicEnemy") || co.gameObject.CompareTag("Boss") && !collided)
        {
            collided = true;

            var enemyatm = co.gameObject.GetComponent<EnemyAttributes>();
            var wenemyatm = co.gameObject.GetComponent<WolfEnemyAttributes>();
            if (enemyatm != null && playeratm != null)
            {
                float damage = playeratm.MagicPower;
                enemyatm.TakeDamage(playeratm.MagicPower);
            }
            else if(wenemyatm != null && playeratm != null)
            {
                float damage = playeratm.MagicPower;
                wenemyatm.TakeDamage(playeratm.MagicPower);
            }
            
            var impact = Instantiate(impactVFX, co.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(impact, impactidie);
            Destroy (gameObject);
            Destroy(gameObject, lifeline);
        }
        //if the projectile collides with any tag other than FireBall or Player then collided is true and the projectil impact effect will play and the projectile will get destroyed, and the impat effect will destroy after impactiidie.
        else if(co.gameObject.tag !="FireBall" && co.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            var impact = Instantiate (impactVFX, co.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy (impact, impactidie);
            Destroy (gameObject);
            Destroy(gameObject,lifeline);
        }
    }

    //method to check if the projectile has already collided
    public bool HasCollided()
    {
        return collided;
    }
}