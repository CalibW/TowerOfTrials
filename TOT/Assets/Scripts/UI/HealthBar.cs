using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;             // Slider for displaying current health
    public Slider easeHealthSlider;         // Slider for smooth health transition
    public float lerpSpeed;                 // Speed of the health bar transition
    public float timeToRecoverHealth;       // Total time to recover health (not currently used)
    public float healthRecoveryRate;        // Rate at which health is recovered
    public float recoveryInterval;           // Interval for health recovery coroutine
    [SerializeField] private PlayerAttributes playeratm; // Reference to player's attributes

    void Start()
    {
        // Initialize the health sliders to the player's maximum health
        healthSlider.maxValue = playeratm.Health; // Set max value of health slider
        easeHealthSlider.maxValue = playeratm.Health; // Set max value of eased health slider
        StartCoroutine(RecoverHealthOverTime()); // Start health recovery coroutine
    }

    void Update()
    {
        // Update the health slider value to match the player's current health
        if (healthSlider.value != playeratm.Health)
        {
            healthSlider.value = playeratm.Health; // Set health slider to player's health
        }

        // Smoothly transition the eased health slider to the current health slider value
        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, healthSlider.value, lerpSpeed); // Lerp to current health value
        }
    }

    // Coroutine to recover health over time
    public IEnumerator RecoverHealthOverTime()
    {
        while (true) // Continuous loop for health recovery
        {
            yield return new WaitForSeconds(recoveryInterval); // Wait for the specified interval
            RecoverHealth(); // Call the method to recover health
        }
    }

    // Method to recover health based on player's endurance
    public void RecoverHealth()
    {
        // Check if current health is less than maximum health
        if (playeratm.Health < playeratm.MaxHealth)
        {
            playeratm.Health += playeratm.Endurance * healthRecoveryRate; // Recover health based on endurance
        }
        else if (playeratm.Health > playeratm.MaxHealth)
        {
            playeratm.Health = playeratm.MaxHealth; // Clamp health to maximum
        }
    }
}
