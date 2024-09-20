using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float lerpSpeed;
    public float timeToRecoverHealth;
    public float healthRecoveryRate;
    public float recoveryInterval;
    public PlayerAttributes playeratm;

    void Start()
    {
        healthSlider.maxValue = playeratm.Health;
        easeHealthSlider.maxValue = playeratm.Health;
        StartCoroutine(RecoverHealthOverTime());
    }

    void Update()
    {
        if (healthSlider.value != playeratm.Health)
        {
            healthSlider.value = playeratm.Health;
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed);
        }
    }

    public IEnumerator RecoverHealthOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(recoveryInterval);
            RecoverHealth();
        }
    }

    public void RecoverHealth()
    {
        if (playeratm.Health < playeratm.MaxHealth)
        {
            playeratm.Health += playeratm.Endurance * healthRecoveryRate;
        }
        else if (playeratm.Health > playeratm.MaxHealth)
        {
            playeratm.Health = playeratm.MaxHealth;
        }
    }
}