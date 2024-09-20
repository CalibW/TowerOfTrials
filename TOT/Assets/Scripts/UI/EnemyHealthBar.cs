using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider;             // Slider for displaying current health
    public Slider easeHealthSlider;         // Slider for smooth health transition
    public float lerpSpeed;                 // Speed of the health bar transition
    public float timeToRecoverHealth;       // Total time to recover health
    public float healthRecoveryRate;        // Rate at which health is recovered
    public float recoveryInterval;           // Interval for health recovery
    public float health;                     // Current health value
    [SerializeField] private EnemyAttributes enemyatm; // Reference to enemy attributes
    [SerializeField] private WolfEnemyAttributes WEnemyAtm; // Reference to wolf enemy attributes

    void Start()
    {
        // Initialize health values from enemy attributes if available
        if (enemyatm != null)
        {
            health = enemyatm.Health; // Set current health to enemy's health
            healthSlider.maxValue = enemyatm.Health; // Set max value of health slider
            easeHealthSlider.maxValue = enemyatm.Health; // Set max value of eased health slider
            healthSlider.value = health; // Set initial health slider value
        }

        // Initialize health values from wolf enemy attributes if available
        if (WEnemyAtm != null)
        {
            health = enemyatm.Health; // Set current health to enemy's health
            healthSlider.maxValue = enemyatm.Health; // Set max value of health slider
            easeHealthSlider.maxValue = enemyatm.Health; // Set max value of eased health slider
            healthSlider.value = health; // Set initial health slider value
        }
    }

    void Update()
    {
        // Update the health slider value to match the enemy's current health
        if (healthSlider.value != enemyatm.Health)
        {
            healthSlider.value = enemyatm.Health; // Set health slider to enemy's health
        }

        // Smoothly transition the eased health slider to the current health slider value
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed); // Lerp to current health value
        }
    }
}
