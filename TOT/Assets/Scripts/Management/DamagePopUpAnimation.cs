using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUpAnimation : MonoBehaviour
{
    // Curves to control the animation of opacity, scale, and height of the damage pop-up
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;
    private TextMeshProUGUI tmp; 
    private float time = 0; 
    private Vector3 origin; 

    private void Awake()
    {
        // Get the TextMeshPro component from the child of this GameObject
        tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        origin = transform.position; // Store the original position
    }

    private void Update()
    {
        // Update the opacity of the text based on the animation curve
        tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(time));
        // Update the scale of the pop-up based on the animation curve
        transform.localScale = Vector3.one * scaleCurve.Evaluate(time);
        // Update the position of the pop-up, adding height based on the animation curve
        transform.position = origin + new Vector3(0, 1 + heightCurve.Evaluate(time), 0);
        time += Time.deltaTime; // Increment the time by the delta time
    }
}
