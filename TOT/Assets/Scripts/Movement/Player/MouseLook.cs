using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity; // Sensitivity of mouse movement
    public float maxSensitivity; // Maximum value for mouse sensitivity
    public float minSensitivity; // Minimum value for mouse sensitivity
    public Slider slider; // Reference to the slider component for adjusting sensitivity
    public Transform playerBody; // Reference to the player's body for rotation
    float xRotation = 0; // Variable to store the vertical rotation angle

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen

        // Initialize the slider value based on the current mouse sensitivity
        slider.value = mouseSensitivity; // Set the slider to the current mouse sensitivity

        slider.minValue = minSensitivity; // Set the minimum value of the slider
        slider.maxValue = maxSensitivity; // Set the maximum value of the slider

        // Add a listener to the slider to update mouse sensitivity in real-time
        slider.onValueChanged.AddListener(AdjustSensitivity); // Call AdjustSensitivity when the slider value changes
    }

    void Update()
    {
        // Get mouse movement input and apply sensitivity
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Vertical mouse movement
        
        // Rotate the player body based on horizontal mouse movement
        playerBody.Rotate(Vector3.up * mouseX);
        
        // Adjust the vertical rotation and clamp it to prevent flipping
        xRotation -= mouseY; // Subtract vertical movement from xRotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp the rotation to prevent over-rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Apply the rotation to the camera
    }

    // This function will be called whenever the slider value is changed
    public void AdjustSensitivity(float newSensitivity)
    {
        mouseSensitivity = newSensitivity; // Update mouse sensitivity based on the slider value
    }
}
