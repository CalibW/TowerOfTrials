// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
// using System;

// public class Abilities : MonoBehaviour
// {
    
//     public Slider dahsSlider;
//     public Slider shooterslider;
//     float dashrate;
//     float firerate;
//     public TMP_Text DashText;
//     public TMP_Text FireBallText;
//     public PlayerMovementTOT playerMovement;
//     public Shooter shooter;
//     // Start is called before the first frame update
//     void Start()
//     {
//         firerate= shooter.fireRate;
//         dashrate = playerMovement.dashRate;
//         dahsSlider.maxValue = dashrate;
//         dahsSlider.value = 0;
//         shooterslider.maxValue = firerate;
//         shooterslider.value = 0;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(playerMovement.ctime > 0 && playerMovement.ctime < dashrate)
//         {
//             DashText.text = MathF.Round(dashrate - playerMovement.ctime, 1).ToString();
//             DashText.gameObject.SetActive(true);
//         }
//         else{
//             DashText.text = 0.ToString();
//             DashText.gameObject.SetActive(false);
//         }

//         if(shooter.ftime > 0 && shooter.ftime < firerate)
//         {
//             FireBallText.text = MathF.Round(firerate - shooter.ftime, 1).ToString();
//             FireBallText.gameObject.SetActive(true);
//         }
//         else
//         {
//             FireBallText.text = 0.ToString();
//             FireBallText.gameObject.SetActive(false);
//         }
//     }
// }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Abilities : MonoBehaviour
{
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
        // Update dash cooldown
        if (playerMovement.ctime > 0 && playerMovement.ctime < dashrate)
        {
            DashText.text = MathF.Round(dashrate - playerMovement.ctime, 1).ToString();
            DashText.gameObject.SetActive(true);
            SetSliderOpacity(dashFillImage, playerMovement.ctime / dashrate);
        }
        else
        {
            DashText.text = "0";
            DashText.gameObject.SetActive(false);
            SetSliderOpacity(dashFillImage, 1); // Full opacity when ready
        }

        // Update fireball cooldown
        if (shooter.ftime > 0 && shooter.ftime < firerate)
        {
            FireBallText.text = MathF.Round(firerate - shooter.ftime, 1).ToString();
            FireBallText.gameObject.SetActive(true);
            SetSliderOpacity(fireballFillImage, shooter.ftime / firerate);
        }
        else
        {
            FireBallText.text = "0";
            FireBallText.gameObject.SetActive(false);
            SetSliderOpacity(fireballFillImage, 1); // Full opacity when ready
        }
    }

    // Function to set the opacity of a slider's fill image based on cooldown progress
    void SetSliderOpacity(Image fillImage, float progress)
    {
        Color newColor = fillImage.color;
        newColor.a = Mathf.Lerp(1, 0, progress); // Opacity decreases as cooldown progresses
        fillImage.color = newColor;
    }
}
