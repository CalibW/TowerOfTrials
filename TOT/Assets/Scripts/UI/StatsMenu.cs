using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsMenu : MonoBehaviour
{
    public bool StatsMenuOpen; // Tracks whether the stats menu is open
    public GameObject statsMenuUI; // UI element for the stats menu
    public GameObject Abilities; // UI element for abilities (hidden when stats menu is open)
    public float StatPoints; // Points available for allocating to stats
    [SerializeField] PlayerAttributes PlayerAtm; // Reference to the player's attributes
    [SerializeField] ManaBar MB; // Reference to the player's mana bar
    [SerializeField] HealthBar HB; // Reference to the player's health bar
    public TMP_Text statPoints; // Text display for available stat points
    public TMP_Text HP; // Text display for health
    public TMP_Text AGI; // Text display for agility
    public TMP_Text ENDU; // Text display for endurance
    public TMP_Text MP; // Text display for magic power
    public TMP_Text MANA; // Text display for mana
    public TMP_Text STR; // Text display for strength
    public TMP_Text LVL; // Text display for level
    public TMP_Text EXP; // Text display for experience
    public TMP_Text Name; // Text display for player name (not used in the current implementation)

    void Start()
    {
        CloseStats(); // Ensure the game starts with the stats menu closed
        StatsMenuOpen = false; // Initialize the menu state as closed
        // Initialize text displays with the player's current attributes
        statPoints.text = StatPoints.ToString();
        HP.text = PlayerAtm.Health.ToString() + "/" + PlayerAtm.MaxHealth.ToString();
        AGI.text = PlayerAtm.Agility.ToString();
        STR.text = PlayerAtm.Strength.ToString();
        ENDU.text = PlayerAtm.Endurance.ToString();
        MP.text = PlayerAtm.MagicPower.ToString();
        MANA.text = PlayerAtm.Mana.ToString() + "/" + PlayerAtm.MaxMana.ToString();
        LVL.text = PlayerAtm.Level.ToString();
        EXP.text = Mathf.RoundToInt(PlayerAtm.Experience).ToString() + "/" + Mathf.RoundToInt(PlayerAtm.ExperienceToNextLevel).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input to toggle the stats menu
        if (Input.GetKeyDown(KeyCode.I) && !StatsMenuOpen) // Change key to "I"
        {
            OpenStats(); // Open the stats menu
        }
        else if (Input.GetKeyDown(KeyCode.I) && StatsMenuOpen)
        {
            CloseStats(); // Close the stats menu
        }
        // Update the text displays with current player stats
        HP.text = PlayerAtm.Health.ToString() + "/" + PlayerAtm.MaxHealth.ToString();
        MANA.text = PlayerAtm.Mana.ToString() + "/" + PlayerAtm.MaxMana.ToString();
        LVL.text = PlayerAtm.Level.ToString();
        statPoints.text = StatPoints.ToString();
        EXP.text = Mathf.RoundToInt(PlayerAtm.Experience).ToString() + "/" + Mathf.RoundToInt(PlayerAtm.ExperienceToNextLevel).ToString();
    }

    // Method to open the stats menu
    public void OpenStats()
    {
        statsMenuUI.SetActive(true);  // Show the stats menu UI
        Abilities.SetActive(false); // Hide abilities UI
        Time.timeScale = 0f;          // Optionally stop time, if needed
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
        StatsMenuOpen = true;         // Update the state to open
    }

    // Method to close the stats menu
    public void CloseStats()
    {
        statsMenuUI.SetActive(false); // Hide the stats menu UI
        Abilities.SetActive(true); // Show abilities UI
        Time.timeScale = 1f;          // Resume time if paused
        StatsMenuOpen = false;        // Update the state to closed
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    // Method to increase the player's strength
    public void Strength()
    {
        if (StatPoints >= 0) // Check if there are stat points available
        {
            PlayerAtm.Strength += 1; // Increase strength
            StatPoints -= 1; // Decrease available stat points
            statPoints.text = StatPoints.ToString(); // Update display
            STR.text = PlayerAtm.Strength.ToString(); // Update strength display
        }
    }

    // Method to increase the player's endurance
    public void Endurance()
    {
        if (StatPoints > 0) // Check if there are stat points available
        {
            PlayerAtm.Endurance += 1; // Increase endurance
            StatPoints -= 1; // Decrease available stat points
            statPoints.text = StatPoints.ToString(); // Update display
            ENDU.text = PlayerAtm.Endurance.ToString(); // Update endurance display
        }
    }

    // Method to increase the player's agility
    public void Agility()
    {
        if (StatPoints > 0) // Check if there are stat points available
        {
            PlayerAtm.Agility += 1; // Increase agility
            StatPoints -= 1; // Decrease available stat points
            statPoints.text = StatPoints.ToString(); // Update display
            AGI.text = PlayerAtm.Agility.ToString(); // Update agility display
        }
    }

    // Method to increase the player's magic power
    public void MagicPower()
    {
        if (StatPoints > 0) // Check if there are stat points available
        {
            PlayerAtm.MagicPower += 1; // Increase magic power
            StatPoints -= 1; // Decrease available stat points
            statPoints.text = StatPoints.ToString(); // Update display
            MP.text = PlayerAtm.MagicPower.ToString(); // Update magic power display
        }
    }

    // Method to increase the player's maximum health
    public void Health()
    {
        if (StatPoints > 0) // Check if there are stat points available
        {
            PlayerAtm.MaxHealth += 5; // Increase max health
            StatPoints -= 1; // Decrease available stat points
            statPoints.text = StatPoints.ToString(); // Update display
            HP.text = PlayerAtm.Health.ToString() + "/" + PlayerAtm.MaxHealth.ToString(); // Update health display
            HB.healthSlider.maxValue = PlayerAtm.MaxHealth; // Update health bar maximum
            HB.easeHealthSlider.maxValue = PlayerAtm.MaxHealth; // Update eased health bar maximum
        }
    }

    // Method to increase the player's maximum mana
    public void Mana()
    {
        if (StatPoints > 0) // Check if there are stat points available
        {
            PlayerAtm.MaxMana += 5; // Increase max mana
            StatPoints -= 1; // Decrease available stat points
            statPoints.text = StatPoints.ToString(); // Update display
            MANA.text = PlayerAtm.Mana.ToString() + "/" + PlayerAtm.MaxMana.ToString(); // Update mana display
            MB.manaSlider.maxValue = PlayerAtm.MaxMana; // Update mana bar maximum
            MB.easeManaSlider.maxValue = PlayerAtm.MaxMana; // Update eased mana bar maximum
        }
    }
}
