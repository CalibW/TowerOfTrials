using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class MainMenu : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        Time.timeScale = 0;
    }
    public void PlayGame ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");;
    } 

    public void LoadSETTINGSMENU()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("SETTINGSMENU");
    } 
    
    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

