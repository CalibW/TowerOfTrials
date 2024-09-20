using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public bool Interactable; // Indicates whether the object can be interacted with
    public Vector3 InteractionSize;  // Use Vector3 to represent the size of the interactable cube
    public GameObject player; // Reference to the player object

    void Start()
    {
        Interactable = false; // Initialize Interactable to false at the start
    }

    void Update()
    {
        CheckInteractable(); // Continuously check if the player is within interaction range
    }

    void CheckInteractable()
    {
        Vector3 playerPosition = player.transform.position; // Get the player's position
        Vector3 objectPosition = transform.position; // Get the object's position

        Bounds bounds = new Bounds(objectPosition, InteractionSize); // Create a bounds object representing the interaction area

        // Check if the player's position is within the bounds of the interactable area
        if (bounds.Contains(playerPosition))
        {
            Interactable = true; // Set Interactable to true if the player is within range
        }
        else
        {
            Interactable = false; // Set Interactable to false if the player is out of range
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Set the color for the Gizmos
        Gizmos.DrawWireCube(transform.position, InteractionSize);  // Draw a cube to represent the interaction area
    }
}
