using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsMenu : MonoBehaviour
{
    public bool StatsMenuOpen;
    public GameObject statsMenuUI;
    public float StatPoints;
    public PlayerAttributes PlayerAtm;
    public ManaBar MB;
    public HealthBar HB;
    public TMP_Text statPoints;
    public TMP_Text HP;
    public TMP_Text AGI;
    public TMP_Text ENDU;
    public TMP_Text MP;
    public TMP_Text MANA;
    public TMP_Text STR;
    public TMP_Text LVL;
    public TMP_Text EXP;
    public TMP_Text Name;


    void Start()
    {
        CloseStats(); // Ensure the game starts with the stats menu closed
        StatsMenuOpen = false;
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

        if (Input.GetKeyDown(KeyCode.I) && !StatsMenuOpen) // Change key to "I"
        {
            // if (StatsMenuOpen)
            // {
            //     CloseStats();
            // }
            // else
            // {
            //     OpenStats();
            // }
            OpenStats();
        }
        else if(Input.GetKeyDown(KeyCode.I) && StatsMenuOpen)
        {
            CloseStats();
        }
        HP.text = PlayerAtm.Health.ToString() + "/" + PlayerAtm.MaxHealth.ToString();
        MANA.text = PlayerAtm.Mana.ToString() + "/" + PlayerAtm.MaxMana.ToString();
        LVL.text = PlayerAtm.Level.ToString();
        statPoints.text = StatPoints.ToString();
        EXP.text = Mathf.RoundToInt(PlayerAtm.Experience).ToString() + "/" + Mathf.RoundToInt(PlayerAtm.ExperienceToNextLevel).ToString();
    }

    public void OpenStats()
    {
        statsMenuUI.SetActive(true);  // Show the stats menu UI
        Time.timeScale = 0f;          // Optionally stop time, if needed
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StatsMenuOpen = true;         // Update the state
    }

    public void CloseStats()
    {
        statsMenuUI.SetActive(false); // Hide the stats menu UI
        Time.timeScale = 1f;          // Resume time, if paused
        StatsMenuOpen = false;        // Update the state
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;       
    }

    public void Strength()
    {
        if(StatPoints >= 0)
        {
            PlayerAtm.Strength += 1;
            StatPoints -= 1;
            statPoints.text = StatPoints.ToString();
            STR.text = PlayerAtm.Strength.ToString();
        }
    }

    public void Endurance()
    {
        if(StatPoints > 0)
        {
            PlayerAtm.Endurance += 1;
            StatPoints -= 1;
            statPoints.text = StatPoints.ToString();
            ENDU.text = PlayerAtm.Endurance.ToString();
        }
    }

    public void Agility()
    {
        if(StatPoints > 0)
        {
            PlayerAtm.Agility += 1;
            StatPoints -= 1;
            statPoints.text = StatPoints.ToString();
            AGI.text = PlayerAtm.Agility.ToString();
        }
    }

    public void MagicPower()
    {
        if(StatPoints > 0)
        {
            PlayerAtm.MagicPower += 1;
            StatPoints -= 1;
            statPoints.text = StatPoints.ToString();
            MP.text = PlayerAtm.MagicPower.ToString();
        }
    }

    public void Health()
    {
        if(StatPoints > 0)
        {
            PlayerAtm.MaxHealth += 5;
            StatPoints -= 1;
            statPoints.text = StatPoints.ToString();
            HP.text = PlayerAtm.Health.ToString() + "/" + PlayerAtm.MaxHealth.ToString();
            HB.healthSlider.maxValue = PlayerAtm.MaxHealth;
            HB.easeHealthSlider.maxValue = PlayerAtm.MaxHealth;
        }
    }

    public void Mana()
    {
        if(StatPoints > 0)
        {
            PlayerAtm.MaxMana += 5;
            StatPoints -= 1;
            statPoints.text = StatPoints.ToString();
            MANA.text = PlayerAtm.Mana.ToString() + "/" + PlayerAtm.MaxMana.ToString();
            MB.manaSlider.maxValue = PlayerAtm.MaxMana;
            MB.easeManaSlider.maxValue = PlayerAtm.MaxMana;
        }
    }
}
