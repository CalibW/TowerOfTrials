using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaSlider;
    public Slider easeManaSlider;
    [SerializeField] PlayerAttributes atm;
    public float manaDashLossRate;
    public float manaFireBallLossRate;
    public float timeToLoseMana;
    public float timeToRecoverMana;
    public float manaRecoveryRate;
    public float lerpSpeed;
    public float recoveryInterval;
    

    void Start()
    {
        manaSlider.maxValue = atm.Mana;
        easeManaSlider.maxValue = atm.Mana;
        StartCoroutine(RecoverManaOverTime());
    }

    void Update()
    {
        if (manaSlider.value != atm.Mana)
        {
            manaSlider.value = atm.Mana;
        }

        if (manaSlider.value != easeManaSlider.value)
        {
            easeManaSlider.value = Mathf.Lerp(easeManaSlider.value, manaSlider.value, lerpSpeed);
        }
    }

    public void loseDashMana(int spent)
    {
        atm.Mana -= spent;
        atm.Mana = Mathf.Clamp(atm.Mana, 0, atm.Mana);
    }

    public void loseShootMana(int spent)
    {
        atm.Mana -= spent;
        atm.Mana = Mathf.Clamp(atm.Mana, 0, atm.Mana);
    }

    public IEnumerator RecoverManaOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(recoveryInterval);
            RecoverMana();
        }
    }

    public void RecoverMana()
    {
        if (atm.Mana < atm.MaxMana)
        {
            atm.Mana += atm.MagicPower * manaRecoveryRate;
        }
        else if (atm.Mana > atm.MaxMana)
        {
            atm.Mana = atm.MaxMana;
        }
    }
}