using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float crouchingspeed, normalHeight, crouchingHeight;
    public float dashingPower = 20f;
    public float dashingTime = 0.3f;
    public float dashingCooldown = 0.75f;
    public float dashRate = 2;
    private float timeToDash;
    public Vector3 offset;
    public Transform player;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float threshold;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isMoving;
    bool isGrounded;
    bool crouching;
    bool dashing = true;
    [SerializeField] PlayerAttributes pa;
    [SerializeField] ManaBar mb;

    // if the player goees below the y threshold teleport them back to where they begun at 0, 2, 0
    void FixedUpdate()
    {
        if(transform.position.y < threshold)
        {
            transform.position = new Vector3(0f, 2f, 0f);
        }
    }   

    // check to see if the player is grounded, and if the player is grounded set the y velocity to -2.
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    // making more variables and assets
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //moving
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * pa.Agility * Time.deltaTime);
         if (Input.GetKeyDown(KeyCode.W))
        {
            isMoving = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            isMoving = false;
        }

        //Crouching
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = !crouching;
        }
        if(crouching == true)
        {
            controller.height= controller.height - crouchingspeed * Time.deltaTime;
            if(controller.height <= crouchingHeight)
            {
                controller.height = crouchingHeight;
            }
        }
        if(crouching == false)
        {
            controller.height= controller.height + crouchingspeed * Time.deltaTime;
            if(controller.height < normalHeight)
            {
                player.position = player.position + offset * Time.deltaTime;

            }
            if(controller.height >= normalHeight)
            {
                controller.height = normalHeight;
            }
        }

        //sprinting
        if(Input.GetKey(KeyCode.LeftShift) & isMoving == true & crouching == false)
        {
            controller.Move(move * (pa.Agility * 1.1f) * Time.deltaTime);
;
        }

        //jumping
        // if jump button is clicked (spcace bar) and the player is grounded jump
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
           
        //dashing
        if(Input.GetKeyDown(KeyCode.C) && dashing && isGrounded == true && crouching == false && Time.time >= timeToDash && pa.Mana >= 5f)
    {
        StartCoroutine(Dash());
        timeToDash = Time.time + 1/dashRate;
        mb.loseDashMana(5);
    }
     else if(Input.GetKeyDown(KeyCode.C) && dashing && isGrounded == true && crouching == false && pa.Mana < 5f)
        {
            Debug.Log("Out of Mana to Dash");
        }

    }

    
    
    private IEnumerator Dash()
    {
        dashing = true;
        velocity = new Vector3(transform.forward.x * dashingPower, 0f, transform.forward.z * dashingPower);
        yield return new WaitForSeconds(dashingTime);
        velocity = Vector3.zero;
        yield return new WaitForSeconds(dashingCooldown);
        dashing = true;
    }
}