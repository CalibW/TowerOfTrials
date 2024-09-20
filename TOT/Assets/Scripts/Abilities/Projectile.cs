using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactVFX;
    public float impactidie;
    public float lifeline;
    public bool collided;
    private PlayerAttributes playeratm;  // Reference to the player's AttributesManager

    void Start()
    {
        // Find the player's AttributesManager in the scene
        playeratm = FindObjectOfType<PlayerAttributes>();
    }

    void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.CompareTag("BasicEnemy") || co.gameObject.CompareTag("Boss") && !collided)
        {
            collided = true;

            // Apply damage to the enemy
            var enemyatm = co.gameObject.GetComponent<EnemyAttributes>();
            var wenemyatm = co.gameObject.GetComponent<WolfEnemyAttributes>();
            if (enemyatm != null && playeratm != null)
            {
                float damage = playeratm.MagicPower;
                enemyatm.TakeDamage(playeratm.MagicPower);  // Ensure TakeDamage accepts int if needed
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
        else if(co.gameObject.tag !="FireBall" && co.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            var impact = Instantiate (impactVFX, co.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy (impact, impactidie);
            Destroy (gameObject);
            Destroy(gameObject,lifeline);
        }
    }

    public bool HasCollided()
    {
        return collided;
    }
}