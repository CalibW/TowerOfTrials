using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaSlider;                // Slider for displaying current mana
    public Slider easeManaSlider;            // Slider for smooth mana transition
    [SerializeField] PlayerAttributes atm;   // Reference to player's attributes
    public float manaDashLossRate;           // Mana loss rate for dashing
    public float manaFireBallLossRate;       // Mana loss rate for fireball
    public float timeToLoseMana;             // Time duration to lose mana (not currently used)
    public float timeToRecoverMana;          // Time duration to recover mana (not currently used)
    public float manaRecoveryRate;           // Rate at which mana is recovered
    public float lerpSpeed;                  // Speed of the mana bar transition
    public float recoveryInterval;            // Interval for mana recovery coroutine

    void Start()
    {
        // Initialize the mana sliders to the player's maximum mana
        manaSlider.maxValue = atm.Mana; // Set max value of mana slider
        easeManaSlider.maxValue = atm.Mana; // Set max value of eased mana slider
        StartCoroutine(RecoverManaOverTime()); // Start mana recovery coroutine
    }

    void Update()
    {
        // Update the mana slider value to match the player's current mana
        if (manaSlider.value != atm.Mana)
        {
            manaSlider.value = atm.Mana; // Set mana slider to player's mana
        }

        // Smoothly transition the eased mana slider to the current mana slider value
        if (manaSlider.value != easeManaSlider.value)
        {
            easeManaSlider.value = Mathf.Lerp(easeManaSlider.value, manaSlider.value, lerpSpeed); // Lerp to current mana value
        }
    }

    // Method to reduce mana when dashing
    public void loseDashMana(int spent)
    {
        atm.Mana -= spent; // Decrease mana by the amount spent
        atm.Mana = Mathf.Clamp(atm.Mana, 0, atm.Mana); // Clamp mana to prevent going below zero
    }

    // Method to reduce mana when shooting
    public void loseShootMana(int spent)
    {
        atm.Mana -= spent; // Decrease mana by the amount spent
        atm.Mana = Mathf.Clamp(atm.Mana, 0, atm.Mana); // Clamp mana to prevent going below zero
    }

    // Coroutine to recover mana over time
    public IEnumerator RecoverManaOverTime()
    {
        while (true) // Continuous loop for mana recovery
        {
            yield return new WaitForSeconds(recoveryInterval); // Wait for the specified interval
            RecoverMana(); // Call the method to recover mana
        }
    }

    // Method to recover mana based on player's magic power
    public void RecoverMana()
    {
        // Check if current mana is less than maximum mana
        if (atm.Mana < atm.MaxMana)
        {
            atm.Mana += atm.MagicPower * manaRecoveryRate; // Recover mana based on magic power
        }
        else if (atm.Mana > atm.MaxMana)
        {
            atm.Mana = atm.MaxMana; // Clamp mana to maximum
        }
    }
}
