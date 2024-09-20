using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotation : MonoBehaviour
{
    public GameObject Portal; // Reference to the portal GameObject
    public float RotateSpeed; // Speed at which the portal will rotate

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the portal around the Z-axis based on the RotateSpeed
        Portal.transform.Rotate(0, 0, RotateSpeed);
    }
}
