using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Track if the game is paused

    public GameObject pauseMenuUI; // Reference to the pause menu UI
    public GameObject settingsMenuUI; // Reference to the settings menu UI

    void Start()
    {
        Resume(); // Ensure the game is not paused at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume(); // Resume the game if it is currently paused
            }
            else
            {
                Pause(); // Pause the game if it is currently running
            }
        }     
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        Time.timeScale = 1f; // Set time scale back to normal
        GameIsPaused = false; // Update pause state
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        Time.timeScale = 0f; // Stop the game time
        GameIsPaused = true; // Update pause state
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
    }

    public void LoadMAINMENU()
    {
        Time.timeScale = 0f; // Ensure time is normal when loading a new scene
        SceneManager.LoadScene("MAINMENU"); // Load the main menu scene
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game..."); // Log quitting message
        Application.Quit(); // Quit the application
    }

    public void LoadSETTINGSMENU()
    {
        Time.timeScale = 0f; // Ensure time is normal when loading a new scene
        SceneManager.LoadScene("SETTINGSMENU"); // Load the settings menu scene
    }

    public void SettingsMenuOpen()
    {
        pauseMenuUI.SetActive(false); // Hide the pause menu UI
        settingsMenuUI.SetActive(true); // Show the settings menu UI
        GameIsPaused = true; // Keep the game paused
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
    }

    public void SettingsMenuClose()
    {
        pauseMenuUI.SetActive(true); // Show the pause menu UI
        settingsMenuUI.SetActive(false); // Hide the settings menu UI
        GameIsPaused = true; // Keep the game paused
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
    }
}
