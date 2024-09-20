using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsInteractable : MonoBehaviour
{
    public GameObject InteractableUI; // UI element for the first interactable prompt
    public GameObject InteractableUI2; // UI element for the second interactable prompt
    public PlayerAttributes playerAttributes; // Reference to the player's attributes
    public Interaction Portal1Interaction; // Reference to the interaction for the first portal
    public Interaction Portal2Interaction; // Reference to the interaction for the second portal

    void Awake()
    {
        // Initially set both UI elements to inactive
        InteractableUI.SetActive(false);
        InteractableUI2.SetActive(false);
    }

    void Start()
    {
        // Ensure both UI elements are inactive at the start
        InteractableUI.SetActive(false);
        InteractableUI2.SetActive(false);
    }

    void Update()
    {
        // Check to open the first interactable UI
        InteractableUIOpenLevel1();
        // Check to load the next level
        LoadNextLevel();
        // Check to open the second interactable UI
        InteractableUIOpenLevel2();
        // Check for escape action
        Escape();
    }

    public void InteractableUIOpenLevel1()
    {
        // Show the first interactable UI if the portal interaction is active
        if(Portal1Interaction.Interactable == true)
        {
            InteractableUI.SetActive(true); // Activate the UI
        }
        else
        {
            InteractableUI.SetActive(false); // Deactivate the UI
        }
    }

    public void InteractableUIOpenLevel2()
    {
        // Show the second interactable UI if the second portal interaction is active
        if(Portal2Interaction.Interactable == true)
        {
            InteractableUI2.SetActive(true); // Activate the UI
        }
        else
        {
            InteractableUI2.SetActive(false); // Deactivate the UI
        }
    }

    void LoadNextLevel()
    {
        // Load the next level if the first portal is interactable and the UI is active
        if(Portal1Interaction.Interactable == true && InteractableUI.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Level 2"); // Load the specified scene
            InteractableUI.SetActive(false); // Deactivate the UI
            Portal1Interaction.Interactable = false; // Set the portal interaction to inactive
        }
    }

    void Escape()
    {
        // Load the winning menu if the second portal is interactable and the UI is active
        if(Portal2Interaction.Interactable == true && InteractableUI2.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("WINNINGMENU"); // Load the winning menu scene
            Time.timeScale = 0; // Pause the game
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible
            Destroy(playerAttributes.gameObject); // Remove the player object from the scene
        }
    }
}
