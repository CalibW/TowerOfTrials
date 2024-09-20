using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Abilities : MonoBehaviour
{
    //Creating variables, linkning other scripts, and accessing UI Elements.
    public Slider dahsSlider;
    public Slider shooterslider;
    public TMP_Text DashText;
    public TMP_Text FireBallText;
    public PlayerMovementTOT playerMovement;
    public Shooter shooter;
    private Image dashFillImage;
    private Image fireballFillImage;

    float dashrate;
    float firerate;

    void Start()
    {
        // Assign cooldown rates
        firerate = shooter.fireRate;
        dashrate = playerMovement.dashRate;

        // Set the max values for the sliders
        dahsSlider.maxValue = dashrate;
        dahsSlider.value = dashrate;
        shooterslider.maxValue = firerate;
        shooterslider.value = firerate;

        // Get the Image component of the fill areas of both sliders
        dashFillImage = dahsSlider.fillRect.GetComponent<Image>();
        fireballFillImage = shooterslider.fillRect.GetComponent<Image>();

    }

    void Update()
    {
        // Update dash cooldown when the ctime is greater than 0 and less than the dashrate
        if (playerMovement.ctime > 0 && playerMovement.ctime < dashrate)
        {
            DashText.text = MathF.Round(dashrate - playerMovement.ctime, 1).ToString();
            DashText.gameObject.SetActive(true);
            SetSliderOpacity(dashFillImage, playerMovement.ctime / dashrate);
        }
        //dash cooldown is 0 with max opacity
        else
        {
            DashText.text = "0";
            DashText.gameObject.SetActive(false);
            SetSliderOpacity(dashFillImage, 1);
        }

        // Update fireball cooldown, wehn ftime is greater than 0 and less than the firerate
        if (shooter.ftime > 0 && shooter.ftime < firerate)
        {
            FireBallText.text = MathF.Round(firerate - shooter.ftime, 1).ToString();
            FireBallText.gameObject.SetActive(true);
            SetSliderOpacity(fireballFillImage, shooter.ftime / firerate);
        }
        //fireball cooldown is 0 and max opacity
        else
        {
            FireBallText.text = "0";
            FireBallText.gameObject.SetActive(false);
            SetSliderOpacity(fireballFillImage, 1);
        }

    }

    // Function to set the opacity of a slider's fill image based on cooldown progress
    void SetSliderOpacity(Image fillImage, float progress)
    {
        Color newColor = fillImage.color;
        newColor.a = Mathf.Lerp(1, 0, progress);
        fillImage.color = newColor;
    }
}


