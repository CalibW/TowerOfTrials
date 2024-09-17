using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotation : MonoBehaviour
{

    public GameObject Portal;
    public float RotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Portal.transform.Rotate(0,0, RotateSpeed);
    }
}
