using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Calib");;
    } 

    public void LoadSETTINGSMENU()
    {
        Time.timeScale = 1f;
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

