using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WolfEnemyHealthBar : MonoBehaviour
{
    public Slider healthSlider; // Slider for the enemy's current health
    public Slider easeHealthSlider; // Slider for a smooth transition of health display
    public float lerpSpeed; // Speed of the smooth transition for the health bar
    public float timeToRecoverHealth; // Total time to recover health (not currently used)
    public float healthRecoveryRate; // Rate of health recovery (not currently used)
    public float recoveryInterval; // Time interval for health recovery (not currently used)
    public float health; // Current health of the enemy
    [SerializeField] private WolfEnemyAttributes WEnemyAtm; // Reference to the Wolf enemy's attributes

    void Start()
    {
        // Initialize health values if the enemy attributes are assigned
        if (WEnemyAtm != null)
        {
            health = WEnemyAtm.Health; // Set current health from attributes
            healthSlider.maxValue = WEnemyAtm.Health; // Set the maximum value of the health slider
            easeHealthSlider.maxValue = WEnemyAtm.Health; // Set the maximum value of the eased health slider
            healthSlider.value = health; // Set the current value of the health slider
        }

        // Duplicate check (can be removed, already handled above)
        if (WEnemyAtm != null)
        {
            health = WEnemyAtm.Health; // Set current health from attributes
            healthSlider.maxValue = WEnemyAtm.Health; // Set the maximum value of the health slider
            easeHealthSlider.maxValue = WEnemyAtm.Health; // Set the maximum value of the eased health slider
            healthSlider.value = health; // Set the current value of the health slider
        }
    }

    void Update()
    {
        // Update health slider if the enemy's current health has changed
        if (healthSlider.value != WEnemyAtm.Health)
        {
            healthSlider.value = WEnemyAtm.Health; // Update the health slider value
        }

        // Smoothly transition the eased health slider towards the current health slider value
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed); // Lerp for smooth transition
        }
    }
}
