using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    UnityEngine.AI.NavMeshAgent agent; // NavMeshAgent component for controlling movement
    Animator animator; // Animator component for controlling animations

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // Get the NavMeshAgent component
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = playerTransform.position; // Set the agent's destination to the player's position
        animator.SetFloat("Speed", agent.velocity.magnitude); // Update the animator with the agent's speed
    }
}
