using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class GuardBehavior : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints for patrolling
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    public float normalSpeed = 3.5f; // Normal patrol speed
    public float boostedSpeed = 7.0f; // Speed when patrolling faster
    public List<GameObject> ventObjects; // List of all vents
    private HashSet<GameObject> openedVents = new HashSet<GameObject>(); // Track opened vents

    void Start()
    {
        // Get the NavMeshAgent component attached to the guard
        agent = GetComponent<NavMeshAgent>();

        // Set the initial speed and start patrolling
        agent.speed = normalSpeed;
        if (waypoints.Length > 0)
        {
            agent.destination = waypoints[currentWaypointIndex].position;
        }
    }

    void Update()
    {
        // Check if the guard has reached its current waypoint
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }

        // Check the state of all vents
        foreach (GameObject vent in ventObjects)
        {
            if (vent != null && !openedVents.Contains(vent))
            {
                // If the vent is open, its X rotation is approximately 90 degrees
                if (Mathf.Approximately(vent.transform.eulerAngles.x, 90f))
                {
                    openedVents.Add(vent); // Mark this vent as opened
                    BoostSpeed(); // Trigger speed boost
                }
            }
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        // Update the waypoint index to the next one in the list
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

        // Set the agent's destination to the next waypoint
        agent.destination = waypoints[currentWaypointIndex].position;
    }

    // Temporarily boost the guard's speed
    void BoostSpeed()
    {
        agent.speed = boostedSpeed;
        Invoke("ResetSpeed", 5f); // Reset the speed after 5 seconds
    }

    // Reset the guard's speed to normal
    void ResetSpeed()
    {
        agent.speed = normalSpeed;
    }
}