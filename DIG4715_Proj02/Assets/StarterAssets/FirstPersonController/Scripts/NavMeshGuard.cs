using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints; // Points for the enemy to patrol
    private int currentPatrolIndex = 0; // Index of the current patrol point
    private NavMeshAgent agent; // NavMeshAgent for the enemy's movement

    public float detectionRadius = 2f; // Radius to detect the player
    private bool isPatrolling = true;

    void Start()
    {
        // Get the NavMeshAgent component on this object
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {
        if (isPatrolling)
        {
            // Check if the enemy has reached the current patrol point
            if (Vector3.Distance(agent.transform.position, patrolPoints[currentPatrolIndex].position) < 0.5f)
            {
                // Move to the next patrol point
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            }
        }

        // Detect if the player is within the detection radius (collision detection approach)
        DetectPlayer();
    }

    // Function to check if the player is within detection radius
    void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))  // Assuming the player has a "Player" tag
            {
                // Trigger "Game Over" when the player touches the enemy
                Debug.Log("Game Over");
                isPatrolling = false; // Stop patrolling when player is detected
                break;
            }
        }
    }

    // Optionally: If you want to reset the patrol after some delay (in case you want a reset mechanism)
    // IEnumerator ResetPatrolAfterGameOver()
    // {
    //     yield return new WaitForSeconds(2f); // Wait for 2 seconds
    //     isPatrolling = true; // Resume patrolling
    // }
}