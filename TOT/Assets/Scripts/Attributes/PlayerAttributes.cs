using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;
using Unity.VisualScripting.Dependencies.NCalc;

public class PlayerAttributes : MonoBehaviour
{

   //creating the stats and variables of the player as well as linkning game object and the other scripts to this script
    public float Level;
    public float Experience;
    public float FirstLevelExperience;
    public float ExperienceToNextLevel;
    public float ExpIncrease;
    public float Health;
    public float MaxHealth;
    public float Mana;
    public float MaxMana;
    public float Strength;
    public float Endurance;
    public float Agility;
    public float MagicPower;
    public float StatPointsGiven;
    public float minRandom;
    public float maxRandom;
    public  GameObject player;
    public EnemyAttributes SlimeATM;
    public WolfEnemyAttributes WolfATM;
    public StatsMenu StatMenu;
    public PauseMenu PauseMenu;
    public ManaBar ManaBar;
    public HealthBar HealthBar;
    public EnemySpawning EnemySpawning;
   private string saveFilePath;

      //when the game awakes load the saved player data
      void Awake()
   {
      LoadPlayerData(); // Automatically load the data when the game starts

   }

   //when the game starts set the max mana as the players mana, and max health to the players healht, as well the experience needed to level up as the first level experience, as wall as set the path to save file
    void Start()
    {
     saveFilePath = Application.persistentDataPath + "/playerSaveData.json";
     MaxMana = Mana;
     MaxHealth = Health;
     ExperienceToNextLevel = FirstLevelExperience;
    }

    void Update()
    { 
      //checks if health is less than or equal to 0 , and if it is load the loosing menu as the payer has died as well as destroy the gameObject(in this case the player)
      if(Health <= 0)
      {
         SceneManager.LoadScene("LOOSINGMENU");
         Time.timeScale = 0;
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
         Destroy(gameObject);
      }
      //if the level of the player is 0 the set the amount of experence needed to level up as the first level experience.
      if(Level == 0)
      {
         ExperienceToNextLevel = FirstLevelExperience;
      }
      //if the player level is greater than 0 and less than 25 then set the experience needed to level up as the first level experience time the exp increase to the power of the players level.
      else if (Level > 0 && Level < 25)
      {
         ExperienceToNextLevel = FirstLevelExperience * Mathf.Pow(ExpIncrease, Level);
      }
      //if the players level is 25 then set the experience needed as 9999999;
      else if(Level == 25)
      {
         ExperienceToNextLevel = 9999999;
      }
      LevelUp();
      
      //if the input is the s key then save the players data
      if (Input.GetKeyDown(KeyCode.S))
    {
        SavePlayerData(); // Call this to save the game
    }
    //if the input key is L load the saved data.
    if (Input.GetKeyDown(KeyCode.L))
    {
        LoadPlayerData(); // Call this to load the game
    }

    // function for the player to take damage and generate the damage pop up numbers
    }
     public void TakeDamage(float amount)
     {
        Health -= amount;
        Health = Mathf.Max(0, Health);
        Vector3 randomness = new Vector3(Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom), Random.Range(minRandom, maxRandom));
        DamagePopUpGenerator.current.CreatePopUp(transform.position + randomness, amount.ToString(), Color.yellow);
     }

      //function to deal damage according to the players strength stat
     public void DealDamage(GameObject target)
     {
        var enemyatm = target.GetComponent<EnemyAttributes>();
        if (enemyatm != null)
        {
          enemyatm.TakeDamage(Strength);
        }
     }

      //function to level up when the expereince the player has exceeds the experience to next level, and when this does increase level by one and statpoint by statpoints given
     public void LevelUp()
     {
         if(Experience >= ExperienceToNextLevel)
      {
         Experience = Experience - ExperienceToNextLevel;
         Level += 1;
         StatMenu.StatPoints += StatPointsGiven;
         Health = MaxHealth;
         Mana = MaxMana;
      }
     }

      // save the data that is linked below
     public void SavePlayerData()
    {
        PlayerSaveData saveData = new PlayerSaveData
        {
            //Player Stats
            PlayerLevel = Level,
            PlayerExperience = Experience,
            PlayerHealth = Health,
            PlayerMaxHealth = MaxHealth,
            PlayerMana = Mana,
            PlayerMaxMana = MaxMana,
            PlayerStrength = Strength,
            PlayerEndurance = Endurance,
            PlayerAgility = Agility,
            PlayerMagicPower = MagicPower,
            PlayerStatPoints = StatMenu.StatPoints,

            //Slime Stats
            SlimeExperienceDropped = SlimeATM.ExperienceDropped,
            SlimeEndurance = SlimeATM.Endurance,
            SlimeAgility = SlimeATM.Agility,
            SlimeMana = SlimeATM.Mana,
            SlimeMagicPower = SlimeATM.MagicPower,
            SlimeStrength = SlimeATM.Strength,
            SlimeHealth = SlimeATM.Health,

            //Wolf Stats
            WolfExperienceDropped = WolfATM.ExperienceDropped,
            WolfEndurance = WolfATM.Endurance,
            WolfAgility = WolfATM.Agility,
            WolfMana = WolfATM.Mana,
            WolfMagicPower = WolfATM.MagicPower,
            WolfStrength = WolfATM.Strength,
            WolfHealth = WolfATM.Health,
            WolfrunSpeed = WolfATM.runSpeed,
            WolfwalkSpeed = WolfATM.walkSpeed,


            //Pause and Stats Menu
            IsStatsMenuOpen = StatMenu.StatsMenuOpen,
            IsGamePaused = PauseMenu.GameIsPaused,

            //HealthBar && ManaBAr
            HealthBarValue = HealthBar.healthSlider.value,
            ManaBarValue = ManaBar.manaSlider.value
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFilePath, json);  // Write JSON to a file
        Debug.Log("Game Saved: " + saveFilePath);
    }

    // Load the player's stats from a JSON file
    public void LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerSaveData loadedData = JsonUtility.FromJson<PlayerSaveData>(json);

            // Assign loaded values to the player's attributes
            Level = loadedData.PlayerLevel;
            Experience = loadedData.PlayerExperience;
            Health = loadedData.PlayerHealth;
            MaxHealth = loadedData.PlayerMaxHealth;
            Mana = loadedData.PlayerMana;
            MaxMana = loadedData.PlayerMaxMana;
            Strength = loadedData.PlayerStrength;
            Endurance = loadedData.PlayerEndurance;
            Agility = loadedData.PlayerAgility;
            MagicPower = loadedData.PlayerMagicPower;
            StatMenu.StatPoints = loadedData.PlayerStatPoints;

            //Assign Slime Stats to Slime
            SlimeATM.ExperienceDropped = loadedData.SlimeExperienceDropped;
            SlimeATM.Endurance = loadedData.SlimeEndurance;
            SlimeATM.Agility = loadedData.SlimeAgility;
            SlimeATM.Mana = loadedData.SlimeMana;
            SlimeATM.MagicPower = loadedData.SlimeMagicPower;
            SlimeATM.Strength = loadedData.SlimeStrength;
            SlimeATM.Health = loadedData.SlimeHealth;

            //Assign Wolf Stats to Wolf
            WolfATM.ExperienceDropped = loadedData.WolfExperienceDropped;
            WolfATM.Endurance = loadedData.WolfEndurance;
            WolfATM.Agility = loadedData.WolfAgility;
            WolfATM.Mana = loadedData.WolfMana;
            WolfATM.MagicPower = loadedData.WolfMagicPower;
            WolfATM.Strength = loadedData.WolfStrength;
            WolfATM.Health = loadedData.WolfHealth;
            WolfATM.walkSpeed = loadedData.WolfwalkSpeed;
            WolfATM.runSpeed = loadedData.WolfrunSpeed;

            //Assign Health and Mana Bar values
            HealthBar.healthSlider.value = loadedData.HealthBarValue;
            ManaBar.manaSlider.value = loadedData.ManaBarValue;

            //Assign Open or not to pasue and stats menu
            StatMenu.StatsMenuOpen = loadedData.IsStatsMenuOpen;
            PauseMenu.GameIsPaused = loadedData.IsGamePaused;

            if(PauseMenu.GameIsPaused)
            {
               PauseMenu.Pause();
            }
            else
            {
               PauseMenu.Resume();
            }
            if(StatMenu.StatsMenuOpen)
            {
               StatMenu.OpenStats();
            }
            else
            {
               StatMenu.CloseStats();
            }

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.LogWarning("No save file found");
        }
    }

   void OnApplicationQuit()
   {
      SavePlayerData();
   }
}

