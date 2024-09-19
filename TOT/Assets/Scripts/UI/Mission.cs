using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Quest : MonoBehaviour
{

    public TMP_Text AmountofEnemiesNeededToKill;
    public TMP_Text AmountofBossesNeededToKill;
    public float BasicQuestKillAmount;
    public float BossQuestKillAmount;
    public GameObject QuestUI1;
    public GameObject QuestUI2;
    public GameObject QuestUI3;
    public GameObject Quest3CompleteUI;
    public GameObject Quest2CompleteUI;
    public GameObject Quest1CompleteUI;
    public GameObject QuestAlertUI;
    public float duration;
    public bool QuestUI1IsOpen;
    public bool QuestUI2IsOpen;
    public bool QuestUI3IsOpen;
    public bool IsQuestCompleted1;
    public bool IsQuestCompleted2;
    public bool IsQuestCompleted3;
    public EnemySpawning EnemySpawning;
    public EnemyAttributes EnemyAttributes;
    public WolfEnemyAttributes WolfEnemyAttributes;
    public Interaction Interaction;
    public GameObject BossMob;
    public GameObject Portal1;
    public GameObject Portal2;
    public GameObject Player;

    void OnEnable()
    {
        ResetQuests();
    }

    void ResetQuests()
    {
        EnemyAttributes.BasicAmountKilled = 0;
        EnemyAttributes.BossKilled = 0;
        WolfEnemyAttributes.BasicAmountKilled = 0;
        WolfEnemyAttributes.BossKilled = 0;
        IsQuestCompleted1 = false;
        IsQuestCompleted2 = false;
        IsQuestCompleted3 = false;

        Quest1CompleteUI.SetActive(false);
        Quest2CompleteUI.SetActive(false);
        Quest3CompleteUI.SetActive(false);
        QuestAlertUI.SetActive(true);
        Portal1.SetActive(false);
        BossMob.SetActive(false);
    }
    void Start()
    {
        QuestUI1Close();
        QuestAlertUIActive();
        QuestUI2Close();
        QuestUI3Close();
        Portal2.SetActive(false);
    }

    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        // Update the quest UI text with the static BasicAmountKilled value
        AmountofEnemiesNeededToKill.text = EnemyAttributes.BasicAmountKilled.ToString() + "/" + BasicQuestKillAmount.ToString();
        AmountofBossesNeededToKill.text = EnemyAttributes.BossKilled.ToString() + "/" + BossQuestKillAmount.ToString();
        if(currentSceneIndex == 2)
        {
            // Toggle the quest UI on/off with the R key
            if (Input.GetKeyDown(KeyCode.R) && QuestUI1IsOpen == false && !IsQuestCompleted1)
            {
                QuestUI1Open();
                QuestAlertUINotActive();
            }
            else if (Input.GetKeyDown(KeyCode.R) && QuestUI1IsOpen == true && !IsQuestCompleted1)
            {
                QuestUI1Close();
            }

            // Check if the basic quest is complete
            if (((EnemyAttributes.BasicAmountKilled >= BasicQuestKillAmount) || (WolfEnemyAttributes.BasicAmountKilled >= BasicQuestKillAmount)) && IsQuestCompleted1 == false)
            {
                Quest1Complete();
            }
            else
            {
                Quest1NotComplete();
            }

            if(Input.GetKeyDown(KeyCode.R) && QuestUI2IsOpen == false && IsQuestCompleted1 == true)
            {
                QuestUI2Open();
                QuestAlertUINotActive();
            }
            else if (Input.GetKeyDown(KeyCode.R) && QuestUI2IsOpen == true && IsQuestCompleted1 == true)
            {
                QuestUI2Close();
            }
            if (((EnemyAttributes.BossKilled >= BossQuestKillAmount) || (WolfEnemyAttributes.BossKilled >= BossQuestKillAmount)) && !IsQuestCompleted2)
            {
                Quest2Complete();
            }
            else
            {
                Quest2NotComplete();
            }

        }



        if(currentSceneIndex == 0)
        {
        if (Input.GetKeyDown(KeyCode.R) && QuestUI1IsOpen == false && !IsQuestCompleted1)
        {
            QuestUI1Open();
            QuestAlertUINotActive();
        }
        else if (Input.GetKeyDown(KeyCode.R) && QuestUI1IsOpen == true && !IsQuestCompleted1)
        {
            QuestUI1Close();
        }
             if(Input.GetKeyDown(KeyCode.R) && QuestUI3IsOpen == false && IsQuestCompleted1 == true)
        {
            QuestUI3Open();
            QuestAlertUINotActive();
        }
        else if (Input.GetKeyDown(KeyCode.R) && QuestUI3IsOpen == true && IsQuestCompleted1 == true)
        {
            QuestUI3Close();
        }
            if(((EnemyAttributes.BasicAmountKilled >= BasicQuestKillAmount) || (WolfEnemyAttributes.BasicAmountKilled >= BasicQuestKillAmount)) && IsQuestCompleted1 == false)
        {
            Quest1CompleteInLevel2();
        }
        else
        {
            Quest1NotCompleteInLevel2();
        }

        if(Interaction.Interactable == true && !IsQuestCompleted3)
        {
            Quest3Complete();
        }
        else
        {
            Quest3NotComplete();
        }

        }


        if(IsQuestCompleted1 == true && IsQuestCompleted2 == true && Input.GetKeyDown(KeyCode.F))
        {
        EnemyAttributes.BasicAmountKilled = 0;
        EnemyAttributes.BossKilled = 0;
        WolfEnemyAttributes.BasicAmountKilled = 0;
        WolfEnemyAttributes.BossKilled = 0;
        EnemySpawning.EnemyCount = 0;
        
        IsQuestCompleted1 = false;
        IsQuestCompleted2 = false;

        Quest1CompleteUI.SetActive(false);
        Quest2CompleteUI.SetActive(false);
        QuestAlertUI.SetActive(true);
        Portal1.SetActive(false);
        BossMob.SetActive(false);
        }
    }


    void QuestUI1Close()
    {
        QuestUI1.SetActive(false);
        Time.timeScale = 1f;
        QuestUI1IsOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void QuestUI1Open()
    {
        QuestUI1.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        QuestUI1IsOpen = true;
    }

     void QuestUI2Close()
    {
        QuestUI2.SetActive(false);
        Time.timeScale = 1f;
        QuestUI2IsOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void QuestUI2Open()
    {
        QuestUI2.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        QuestUI2IsOpen = true;
    }

     void QuestUI3Close()
    {
        QuestUI3.SetActive(false);
        Time.timeScale = 1f;
        QuestUI3IsOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void QuestUI3Open()
    {
        QuestUI3.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        QuestUI3IsOpen = true;
    }

    void QuestAlertUIActive()
    {
        QuestAlertUI.SetActive(true);
    }

    void QuestAlertUINotActive()
    {
        QuestAlertUI.SetActive(false);
    }

    public void Quest1Complete()
    {
        IsQuestCompleted1 = true;
        QuestAlertUI.SetActive(true);
        Quest1CompleteUI.SetActive(true);
        StartCoroutine(DelayDeactivateQuest1Complete());
        BossMob.SetActive(true);
    }

    public void Quest1NotComplete()
    {
        if(!IsQuestCompleted1)
        {
            Quest1CompleteUI.SetActive(false);
            BossMob.SetActive(false);
        }
    }

    public void Quest1CompleteInLevel2()
    {
        IsQuestCompleted1 = true;
        QuestAlertUI.SetActive(true);
        Quest1CompleteUI.SetActive(true);
        StartCoroutine(DelayDeactivateQuest1Complete());
        Portal2.SetActive(true);
    }

    public void Quest1NotCompleteInLevel2()
    {
        if(!IsQuestCompleted1)
        {
            Quest1CompleteUI.SetActive(false);
            Portal2.SetActive(false);
        }
    }

    public void Quest2Complete()
    {
        IsQuestCompleted2 = true;
        Quest2CompleteUI.SetActive(true);
        Portal1.SetActive(true);
        StartCoroutine(DelayDeactivateQuest2Complete());
    }

    public void Quest2NotComplete()
    {
        if(!IsQuestCompleted2)
        {
            Quest2CompleteUI.SetActive(false);
            Portal1.SetActive(false);
        }
    }

    public void Quest3Complete()
    {
        IsQuestCompleted3 = true;
        Quest3CompleteUI.SetActive(true);
        StartCoroutine(DelayDeactivateQuest3Complete());
    }

    public void Quest3NotComplete()
    {
        if(!IsQuestCompleted3)
        {
            Quest3CompleteUI.SetActive(false);
        }
    }

    IEnumerator DelayDeactivateQuest3Complete()
    {
        yield return new WaitForSeconds(duration);

        Quest3CompleteUI.SetActive(false);
    }

    IEnumerator DelayDeactivateQuest2Complete()
    {
        yield return new WaitForSeconds(duration);

        Quest2CompleteUI.SetActive(false);
    }

    IEnumerator DelayDeactivateQuest1Complete()
    {
        yield return new WaitForSeconds(duration);

        Quest1CompleteUI.SetActive(false);
    }
}
