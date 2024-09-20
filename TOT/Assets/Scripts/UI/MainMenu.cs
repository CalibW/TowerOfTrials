using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0; // Pause the game when the main menu is opened
    }

    void Start()
    {
        Time.timeScale = 0; // Ensure the game is paused at the start
    }

    // Method to start the game by loading the first level
    public void PlayGame()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("Level 1"); // Load the specified scene
    }

    // Method to load the settings menu
    public void LoadSETTINGSMENU()
    {
        Time.timeScale = 0; // Pause the game when loading settings menu
        SceneManager.LoadScene("SETTINGSMENU"); // Load the settings menu scene
    }

    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("QUIT!"); // Log a message to the console
        Application.Quit(); // Exit the application
    }

    // Method for debugger display (used for debugging purposes)
    private string GetDebuggerDisplay()
    {
        return ToString(); // Return the string representation of the object
    }
}
