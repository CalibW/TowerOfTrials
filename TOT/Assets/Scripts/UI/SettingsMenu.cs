// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SettingsMenu : MonoBehaviour
// {
//     public void SetVolume (float volume)
//     {
//         Debug.Log(volume);
//     }

//     public void MBack()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene("MAINMENU");
//     }

//     public void JBack()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene("Jack");
//     }

//     public void CBack()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadScene("Calib");
//     }

// }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SettingsMenu : MonoBehaviour
{
    public void LoadMAINMENU()
    {
        SceneManager.LoadScene("MAINMENU");
    }
}
