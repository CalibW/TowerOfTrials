using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    // Method to load the main menu scene
    public void LoadMAINMENU()
    {
        SceneManager.LoadScene("MAINMENU"); // Load the main menu scene
        Time.timeScale = 0; // Pause the game (optional, if you want the game to stay paused)
    }
}
