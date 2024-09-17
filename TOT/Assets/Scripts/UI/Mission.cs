using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mission : MonoBehaviour
{
    public TMP_Text AMountofEnemiesNeededToKill;
    public float QuestKillAmount;
    [SerializeField] EnemyAttributes Enemy;
    public GameObject QuestUI;
    public GameObject QuestCompleteUI;
    public float duration;
    public bool QuestUIIsOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        QuestUIClose();
    }

    // Update is called once per frame
    void Update()
    {
        AMountofEnemiesNeededToKill.text = Enemy.AmountKilled.ToString() + "/" + QuestKillAmount.ToString();

        if(Input.GetKeyDown(KeyCode.R) && QuestUIIsOpen == false)
        {
            QuestUIOpen();
        }
        else if(Input.GetKeyDown(KeyCode.R) && QuestUIIsOpen == true)
        {
            QuestUIClose();
        }

        if(Enemy.AmountKilled == QuestKillAmount)
        {
            QuestComplete();
            Destroy(QuestCompleteUI, duration);
        }
        else
        {
            QuestNotComplete();
        }
    }


    void QuestUIClose()
    {
        QuestUI.SetActive(false); // Hide the stats menu UI
        Time.timeScale = 1f;          // Resume time, if paused
        QuestUIIsOpen = false;        // Update the state
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    void QuestUIOpen()
    {
        
        QuestUI.SetActive(true);  // Show the stats menu UI
        Time.timeScale = 0f;          // Optionally stop time, if needed
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        QuestUIIsOpen = true;
    }

    public void QuestComplete()
    {
        QuestCompleteUI.SetActive(true);
    }

    public void QuestNotComplete()
    {
        QuestCompleteUI.SetActive(false);
    }


}
