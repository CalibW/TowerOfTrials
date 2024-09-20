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
    public EnemyAttributes enemyatm;
    public WolfEnemyAttributes WEnemyAtm;

    void Start()
    {
        if (enemyatm != null)
        {
            health = enemyatm.Health;
            healthSlider.maxValue = enemyatm.Health;
            easeHealthSlider.maxValue = enemyatm.Health;
            healthSlider.value = health;
        }

        if (WEnemyAtm != null)
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