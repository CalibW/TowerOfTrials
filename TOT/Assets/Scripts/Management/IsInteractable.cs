using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IsInteractable : MonoBehaviour
{
    public GameObject InteractableUI;
    public GameObject InteractableUI2;
    public Interaction Portal1Interaction;
    public Interaction Portal2Interaction;

    void Awake()
    {
        InteractableUI.SetActive(false);
        InteractableUI2.SetActive(false);
    }

    void Start()
    {
        InteractableUI.SetActive(false);
        InteractableUI2.SetActive(false);
    }

    void Update()
    {
        InteractableUIOpenLevel1();
        LoadNextLevel();
        InteractableUIOpenLevel2();
        Escape();
    }

    public void InteractableUIOpenLevel1()
    {
        if(Portal1Interaction.Interactable == true)
        {
        InteractableUI.SetActive(true);
        }
        else
        {
        InteractableUI.SetActive(false);
        }
    }
    public void InteractableUIOpenLevel2()
    {
        if(Portal2Interaction.Interactable == true)
        {
        InteractableUI2.SetActive(true);
        }
        else
        {
        InteractableUI2.SetActive(false);
        }
    }

    void LoadNextLevel()
    {
        if(Portal1Interaction.Interactable == true && InteractableUI.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Calib");
            InteractableUI.SetActive(false);
            Portal1Interaction.Interactable = false;
        }
    }

    void Escape()
    {
        if(Portal2Interaction.Interactable == true && InteractableUI2.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("WINNINGMENU");
            InteractableUI2.SetActive(false);
            Portal2Interaction.Interactable = false;
        }
    }
}
