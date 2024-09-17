// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class EnemyHealthBar : MonoBehaviour
// {
//     public Slider healthSlider;
//     public Slider easeHealthSlider;
//     public float enemyHealth;
//     public float lerpSpeed;
//     public float timeToRecoverHealth;
//     public float healthRecoveryRate;
//     public float recoveryInterval;
//     [SerializeField] AttributesManager playeratm;
//     [SerializeField] AttributesManager enemyatm;
//      public Projectile prj;

//     // Start is called before the first frame update
//     void Start()
//     {
//      enemyHealth = enemyatm.health; 
//      healthSlider.maxValue = enemyatm.health;
//      easeHealthSlider.maxValue = enemyatm.health;
//      if (prj == null)
//         {
//             prj = FindObjectOfType<Projectile>();
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(healthSlider.value != enemyHealth)
//         {
//             healthSlider.value = enemyHealth;
//         }

//         if(Input.GetKeyDown(KeyCode.Q) )
//         {
//             takeDamage(playeratm.magicPower);
//         }
 
//         if(healthSlider.value != easeHealthSlider.value)
//         {
//             easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
//         }

//          if (prj != null && prj.HasCollided())
//         {
//             Debug.Log("Projectile has collided!");
//         }
//     }

//     void takeDamage(float damage)
//     {
//         enemyHealth -= damage; 
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float lerpSpeed;
    public float timeToRecoverHealth;
    public float healthRecoveryRate;
    public float recoveryInterval;
    public float health;
    [SerializeField] private EnemyAttributes enemyatm;

    void Start()
    {
        if (enemyatm != null)
        {
            health = enemyatm.Health;
            healthSlider.maxValue = enemyatm.Health;
            easeHealthSlider.maxValue = enemyatm.Health;
            healthSlider.value = health;
        }
    }

    void Update()
    {
        if (healthSlider.value != enemyatm.Health)
        {
            healthSlider.value = enemyatm.Health;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }
}