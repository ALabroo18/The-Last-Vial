using System.Collections;
using UnityEngine;

public class VentInteraction : MonoBehaviour
{
    public GameObject popupText;  // UI Text for "Press E to Open"
    public GameObject[] waypoints; // Array to hold waypoints
    private MeshRenderer ventRenderer;
    private Collider ventCollider;
    private bool isOpen = false;
    private Transform playerTransform;

    private bool isCooldown = false; // Cooldown flag
    private float cooldownTime = 3f; // Cooldown duration (in seconds)

    void Start()
    {
        // Get Components
        ventRenderer = GetComponent<MeshRenderer>();
        ventCollider = GetComponent<Collider>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform; // Find the player

        if (playerTransform == null)
        {
            Debug.LogError("Player object not found!");
        }

        // Ensure popup text is disabled at start
        if (popupText != null)
            popupText.SetActive(false);
    }

    void Update()
    {
        // Check if the player is looking at the vent
        if (IsPlayerLookingAtVent() && !isOpen && ventRenderer.enabled)
        {
            popupText.SetActive(true);

            // Check if player presses 'E'
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed, starting OpenVent coroutine.");
                StartCoroutine(OpenVent());
            }
        }
        else
        {
            popupText.SetActive(false);
        }
    }

    IEnumerator OpenVent()
    {
        isOpen = true;

        // Hide vent and disable collision
        SetVentState(false);

        // **Teleport player to random waypoint**
        TeleportToRandomWaypoint();

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Restore vent visibility and collision
        SetVentState(true);
        isOpen = false;
    }

    void SetVentState(bool state)
    {
        ventRenderer.enabled = state;
        ventCollider.enabled = state;
    }

    void TeleportToRandomWaypoint()
    {
        if (waypoints.Length > 0 && playerTransform != null)
        {
            int randomIndex = Random.Range(0, waypoints.Length); // Pick a random waypoint
            Debug.Log($"Teleporting player to waypoint: {waypoints[randomIndex].name}, Position: {waypoints[randomIndex].transform.position}");

            playerTransform.position = waypoints[randomIndex].transform.position; // Move player
        }
        else
        {
            Debug.LogWarning("Waypoints not assigned or player not found!");
        }
    }

    // Trigger method for collision with waypoints
    void OnTriggerEnter(Collider other)
    {
        if (isCooldown)
            return; // If cooldown is active, ignore the collision

        // Check if the object collided with a waypoint (you can tag waypoints to make this check more specific)
        if (other.CompareTag("Waypoint"))
        {
            Debug.Log("Player collided with a waypoint!");

            // Start the teleportation process
            TeleportToRandomWaypoint();

            // Start cooldown coroutine
            StartCoroutine(CooldownRoutine());
        }
    }

    IEnumerator CooldownRoutine()
    {
        isCooldown = true;
        Debug.Log("Cooldown started.");

        // Wait for the cooldown time
        yield return new WaitForSeconds(cooldownTime);

        isCooldown = false;
        Debug.Log("Cooldown ended.");
    }

    bool IsPlayerLookingAtVent()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f)) // 5f is the max detection range
        {
            return hit.collider.gameObject == gameObject; // Check if the vent is being looked at
        }
        return false;
    }
}