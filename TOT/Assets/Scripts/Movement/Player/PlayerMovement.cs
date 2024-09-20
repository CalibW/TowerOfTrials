using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTOT : MonoBehaviour
{
    public CharacterController controller;  // Reference to the CharacterController component
    public float gravity = -9.81f;          // Gravity value applied to the player
    public float jumpHeight = 3f;           // Height of the player's jump
    public float crouchingspeed, normalHeight, crouchingHeight; // Crouching speed and height parameters
    public float dashingPower = 20f;        // Power of the dash movement
    public float dashingTime = 0.3f;        // Duration of the dash
    public float dashingCooldown = 0.75f;   // Cooldown time before the next dash can be initiated
    public float ctime;                     // Timer for dashing cooldown
    public float dashRate = 2;              // Rate at which dashing can be initiated
    public Vector3 offset;                  // Offset for player position during crouch
    public Transform player;                // Reference to the player's transform
    public Transform groundCheck;           // Position used to check if the player is grounded
    public float groundDistance = 0.4f;     // Distance used for ground checking
    public float threshold;                  // Y position threshold for teleporting the player
    public LayerMask groundMask;            // Layer mask to determine what is considered ground
    Vector3 velocity;                       // Current velocity of the player
    bool isMoving;                          // Flag to check if the player is moving
    bool isGrounded;                        // Flag to check if the player is grounded
    bool crouching;                         // Flag to check if the player is crouching
    bool dashing = true;                   // Flag to check if the player is able to dash
    public GameObject SpawnPoint;          // GameObject that sets teh spawnpoint
    public PlayerAttributes pa;             // Reference to the PlayerAttributes script
    public ManaBar mb;                      // Reference to the ManaBar script

    // If the player goes below the y threshold, teleport them back to starting position (0, 30, -452)
    void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = SpawnPoint.transform.position;
        }
    }

    // Update method to check grounded status and handle player movement
    void Update()
    {
        ctime += Time.deltaTime; // Increment the dash cooldown timer
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if player is grounded

        // If grounded, set a small negative y velocity to keep the player grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement input
        float x = Input.GetAxis("Horizontal"); // Horizontal movement input
        float z = Input.GetAxis("Vertical");   // Vertical movement input

        // Calculate movement vector
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * pa.Agility * Time.deltaTime); // Move the player based on input and agility

        // Update movement state
        if (Input.GetKeyDown(KeyCode.W))
        {
            isMoving = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isMoving = false;
        }

        // Crouching logic
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching; // Toggle crouching state
        }

        // Adjust controller height for crouching
        if (crouching == true)
        {
            controller.height = controller.height - crouchingspeed * Time.deltaTime; // Decrease height
            if (controller.height <= crouchingHeight)
            {
                controller.height = crouchingHeight; // Clamp to minimum height
            }
        }
        else // If not crouching
        {
            controller.height = controller.height + crouchingspeed * Time.deltaTime; // Increase height
            if (controller.height < normalHeight)
            {
                player.position = player.position + offset * Time.deltaTime; // Adjust player position upward
            }
            if (controller.height >= normalHeight)
            {
                controller.height = normalHeight; // Clamp to normal height
            }
        }

        // Sprinting logic
        if (Input.GetKey(KeyCode.LeftShift) && isMoving == true && crouching == false)
        {
            controller.Move(move * (pa.Agility * 1.1f) * Time.deltaTime); // Move faster when sprinting
        }

        // Jumping logic
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // Calculate jump velocity
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity

        controller.Move(velocity * Time.deltaTime); // Move the player with the updated velocity

        // Dashing logic
        if (Input.GetKeyDown(KeyCode.C) && dashing && isGrounded == true && crouching == false && ctime > dashRate && pa.Mana >= 5f)
        {
            ctime = 0; // Reset the dash timer
            StartCoroutine(Dash()); // Start the dash coroutine
            
            mb.loseDashMana(5); // Decrease the player's mana for dashing
        }
        else if (Input.GetKeyDown(KeyCode.C) && dashing && isGrounded == true && crouching == false && pa.Mana < 5f)
        {
            Debug.Log("Out of Mana to Dash"); // Log message if out of mana
        }
    }

    // Coroutine to handle the dash action
    private IEnumerator Dash()
    {
        dashing = true; // Set dashing to true
        velocity = new Vector3(transform.forward.x * dashingPower, 0f, transform.forward.z * dashingPower); // Set dash velocity
        yield return new WaitForSeconds(dashingTime); // Wait for the duration of the dash
        velocity = Vector3.zero; // Reset velocity
        yield return new WaitForSeconds(dashingCooldown); // Wait for cooldown
        dashing = true; // Allow dashing again
    }
}
