using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IsInteractable : MonoBehaviour
{
    public GameObject InteractableUI;
    [SerializeField] Interaction PortalInteraction;
    [SerializeField] Interaction TestInteraction;

    void Start()
    {
        InteractableUI.SetActive(false);
    }

    void Update()
    {
        InteractableUIOpen();
        LoadNextLevel();
    }

    public void InteractableUIOpen()
    {
        if(PortalInteraction.Interactable == true || TestInteraction.Interactable == true)
        {
        InteractableUI.SetActive(true);
        }
        else
        {
        InteractableUI.SetActive(false);
        }
    }

    void LoadNextLevel()
    {
        if(PortalInteraction.Interactable == true || TestInteraction.Interactable == true && InteractableUI.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Level 2");
        }
    }
}
