using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WolfEnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float lerpSpeed;
    public float timeToRecoverHealth;
    public float healthRecoveryRate;
    public float recoveryInterval;
    public float health;
    [SerializeField] private WolfEnemyAttributes WEnemyAtm;

    void Start()
    {
        if (WEnemyAtm != null)
        {
            health = WEnemyAtm.Health;
            healthSlider.maxValue = WEnemyAtm.Health;
            easeHealthSlider.maxValue = WEnemyAtm.Health;
            healthSlider.value = health;
        }

        if (WEnemyAtm != null)
        {
            health = WEnemyAtm.Health;
            healthSlider.maxValue = WEnemyAtm.Health;
            easeHealthSlider.maxValue = WEnemyAtm.Health;
            healthSlider.value = health;
        }
    }

    void Update()
    {
        if (healthSlider.value != WEnemyAtm.Health)
        {
            healthSlider.value = WEnemyAtm.Health;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }
}