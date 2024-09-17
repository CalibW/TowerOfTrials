using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool Interactable;
    public Vector3 InteractionSize;  // Use Vector3 to represent the size of the interactable cube
    public GameObject player;

    void Start()
    {
        Interactable = false;
    }

    void Update()
    {
        CheckInteractable();
    }

    void CheckInteractable()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 objectPosition = transform.position;

        Bounds bounds = new Bounds(objectPosition, InteractionSize);

        if (bounds.Contains(playerPosition))
        {
            Interactable = true;
        }
        else
        {
            Interactable = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, InteractionSize);  // Draw a cube to represent the interaction area
    }
}


