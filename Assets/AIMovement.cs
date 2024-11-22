using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI
;
public class AIMovement : MonoBehaviour
{
    private NavMeshAgent agent;  // Reference to the NavMeshAgent component
    private bool isMoving = true;  // Flag to track if AI is moving
    // Start is called before the first frame update
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
    public bool IsAIMoving()
    {
        return isMoving;  // Return whether the AI is currently moving
    }
}
