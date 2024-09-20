using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; // Reference to the volume slider UI element

    void Start()
    {
        // Initialize the slider value with the current audio volume
        volumeSlider.value = AudioListener.volume;

        // Add a listener to the slider to adjust the volume in real-time when the value changes
        volumeSlider.onValueChanged.AddListener(AdjustVolume);
    }

    // Method to adjust the game volume based on the slider value
    public void AdjustVolume(float volume)
    {
        AudioListener.volume = volume; // Set the audio listener's volume to the new value
    }
}
