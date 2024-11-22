using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;         // Reference to the player
    public float roamRadius = 10f;   // How far the AI can roam randomly
    public float roamTime = 5f;      // Time before finding a new target position
    public float followRange = 15f;  // Distance within which the AI will follow the player

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = roamTime;  // Start the timer

        // Optionally, find the player by tag if not set in the inspector
        if (player == null)
        {
            player = GameObject.FindWithTag("Finish").transform;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Follow the player if within follow range, otherwise roam
        if (distanceToPlayer <= followRange)
        {
            FollowsPlayer();
        }
        else if (timer >= roamTime && (!agent.hasPath || agent.remainingDistance < 0.5f))
        {
            Vector3 newPos = GetRandomPosition(transform.position, roamRadius);
            agent.SetDestination(newPos);  // Move character to new random position
            timer = 0;  // Reset the timer
        }
    }

    void FollowsPlayer()
    {
        // Set the destination to the player's position to follow them
        agent.SetDestination(player.position);
    }

    // Get a random position within a certain radius
    Vector3 GetRandomPosition(Vector3 origin, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;  // Pick random point in a sphere
        randomDirection += origin;  // Offset by current position

        NavMeshHit hit;
        // Try to find a point on the NavMesh, if it fails return current position
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;  // Return this valid point
        }
        return origin;  // If no valid position is found, return current position
    }

    // Draw the roaming radius and follow range in the editor as gizmos for visualization
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, roamRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);
    }
}