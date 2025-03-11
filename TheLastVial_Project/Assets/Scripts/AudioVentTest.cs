using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioVentTest : MonoBehaviour
{
    public TextMeshProUGUI uiInteractionText;
    public GameObject interactablePrefab;

    public float xMovementOffset = 3;
    public int yMovementOffset = 0;

    // Audio Source
    [SerializeField] private AudioSource ventSoundSource;

    // Bools to check collision and state
    private bool hasCollided = false;
    private bool hasMoved = false;
    private bool hasRotated = false;

    // Vent Interaction
    [SerializeField] private bool isVentObject = false;
    Quaternion newRotation;

    // Saves rotation of x-axis before it gets rotated
    float originalXRotation;

    // Death Screen
    MainMenu gameMainMenu;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the vent sound does not play when the game starts
        if (ventSoundSource != null && ventSoundSource.isPlaying)
        {
            ventSoundSource.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided && Input.GetKeyDown(KeyCode.E))
        {
            if (interactablePrefab != null)
            {
                if (isVentObject && !hasRotated)
                {
                    originalXRotation = interactablePrefab.transform.eulerAngles.x;
                    newRotation = Quaternion.Euler(90, interactablePrefab.transform.eulerAngles.y, interactablePrefab.transform.eulerAngles.z);
                    interactablePrefab.transform.rotation = newRotation;
                    hasRotated = true;

                    // Play vent sound
                    if (ventSoundSource != null)
                    {
                        ventSoundSource.Play();
                    }
                }
                else if (isVentObject && hasRotated)
                {
                    newRotation = Quaternion.Euler(originalXRotation, interactablePrefab.transform.eulerAngles.y, interactablePrefab.transform.eulerAngles.z);
                    interactablePrefab.transform.rotation = newRotation;
                    hasRotated = false;

                    // Play vent sound
                    if (ventSoundSource != null)
                    {
                        ventSoundSource.Play();
                    }
                }
                else
                {
                    if (!hasMoved)
                    {
                        Debug.Log("hasMoved is false");
                        interactablePrefab.transform.position = new Vector3(
                            interactablePrefab.transform.position.x + xMovementOffset,
                            interactablePrefab.transform.position.y,
                            interactablePrefab.transform.position.z
                        );
                        hasMoved = true;
                    }
                    else if (hasMoved)
                    {
                        Debug.Log("hasMoved is true");
                        interactablePrefab.transform.position = new Vector3(
                            interactablePrefab.transform.position.x - xMovementOffset,
                            interactablePrefab.transform.position.y,
                            interactablePrefab.transform.position.z
                        );
                        hasMoved = false;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Whiteboard"))
        {
            Debug.Log("Whiteboard interacted");
            uiInteractionText.text = "Press E to Move Board";
            interactablePrefab = collision.gameObject;
            hasCollided = true;
        }
        else if (collision.gameObject.CompareTag("Vent"))
        {
            uiInteractionText.text = "Press E to Open/Close Vent";
            interactablePrefab = collision.gameObject;
            hasCollided = true;
            isVentObject = true;

            // Assign vent's AudioSource if not already assigned
            if (ventSoundSource == null)
            {
                ventSoundSource = interactablePrefab.GetComponent<AudioSource>();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision exited");
        uiInteractionText.text = "";
        hasCollided = false;
    }
}   