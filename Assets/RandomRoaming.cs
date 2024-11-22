using UnityEngine;
using UnityEngine.AI;

public class RandomRoaming : MonoBehaviour
{
    public float roamRadius = 10f;  // How far the character can roam
    public float roamTime = 5f;     // Time before finding a new target position

    private NavMeshAgent agent;
    private float timer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = roamTime;  // Start the timer
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= roamTime)
        {
            Vector3 newPos = GetRandomPosition(transform.position, roamRadius);
            agent.SetDestination(newPos);  // Move character to new random position
            timer = 0;  // Reset the timer
        }
    }

    // Get a random position within a certain radius
    Vector3 GetRandomPosition(Vector3 origin, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;  // Pick random point in a sphere
        randomDirection += origin;  // Offset by current position

        NavMeshHit hit;  // Store the NavMesh hit result
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);  // Find nearest valid point on NavMesh
        return hit.position;  // Return this valid point
    }
}
