using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBillboarding : MonoBehaviour
{
    private Camera cam; // Reference to the main camera

    private void Awake()
    {
        // Get the main camera at the start
        cam = Camera.main;
    }

    private void Update()
    {
        // Make the UI element face the camera by setting its forward direction
        transform.forward = cam.transform.forward;
    }
}
