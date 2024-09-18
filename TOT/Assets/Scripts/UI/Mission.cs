using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{

    public TMP_Text AmountofEnemiesNeededToKill;
    public TMP_Text AmountofBossesNeededToKill;
    public float BasicQuestKillAmount;
    public float BossQuestKillAmount;
    public GameObject QuestUI1;
    public GameObject QuestUI2;
    public GameObject Quest2CompleteUI;
    public GameObject Quest1CompleteUI;
    public GameObject QuestAlertUI;
    public float duration;
    public bool QuestUI1IsOpen;
    public bool QuestUI2IsOpen;
    public bool IsQuestCompleted1;
    public bool IsQuestCompleted2;
    [SerializeField] EnemyAttributes EnemyAttributes;
    [SerializeField] WolfEnemyAttributes WolfEnemyAttributes;
    public GameObject BossMob;
    public GameObject Portal;

    void Start()
    {
        QuestUI1Close();
        QuestAlertUIActive();
        QuestUI2Close();
    }

    void Update()
    {
        // Update the quest UI text with the static BasicAmountKilled value
        AmountofEnemiesNeededToKill.text = EnemyAttributes.BasicAmountKilled.ToString() + "/" + BasicQuestKillAmount.ToString();
        AmountofBossesNeededToKill.text = EnemyAttributes.BossKilled.ToString() + "/" + BossQuestKillAmount.ToString();

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
        Destroy(Quest1CompleteUI, duration);
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

    public void Quest2Complete()
    {
        IsQuestCompleted2 = true;
        Quest2CompleteUI.SetActive(true);
        Portal.SetActive(true);
        Destroy(Quest2CompleteUI, duration);
    }

    public void Quest2NotComplete()
    {
        if(!IsQuestCompleted2)
        {
            Quest2CompleteUI.SetActive(false);
            Portal.SetActive(false);
        }
    }
}