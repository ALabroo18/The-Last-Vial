

using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class InteractObject : MonoBehaviour
{
    public TextMeshProUGUI uiInteractionText;
    private GameObject interactablePrefab;

    public CinemachineVirtualCamera myCamera;

    private float xMovementOffset = 0;
    private float yMovementOffset = 0;

    // Audio Source
    [SerializeField] private AudioSource ventSoundSource;

    // Bools to check collision and state
    private bool hasCollided = false;
    private bool hasMoved = false;
    private bool hasRotated = false;

    PushValue op;


    // Vent Interaction
    [SerializeField] private bool isVentObject = false;
    Quaternion newRotation;

    // Saves rotation of x-axis before it gets rotated
    float originalXRotation;

    // Death Screen
    MainMenu gameMainMenu;
    public AudioClip ventClip;

    public FirstPersonController fpc;

    // Bool to check if play is inside vent

    private bool isIn = false;

    // Start is called before the first frame update
    void Start()
    {

        // Gets the first person player script to be used for playing sound
        fpc = this.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((hasCollided && Input.GetButtonDown("Fire1")) && interactablePrefab != null)
        {
            ObjectState();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CompareObject(collision);
    }

    private void OnCollisionExit(Collision collision)
    {

        // Changes variables for when player leaves collision
        Debug.Log("Collision exited");
        uiInteractionText.text = "";
        hasCollided = false;
        if(collision.gameObject.CompareTag("Vent")) {
            isVentObject = false;
        }
    }





// Changes the Cinemachine vertical Arm Length depending on condition
    private void ChangeCamera()
    {
        Cinemachine3rdPersonFollow followComponent = myCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        if (followComponent.VerticalArmLength == 0.75f)
        {
            followComponent.VerticalArmLength = 0.38f;
        }
        else if (followComponent.VerticalArmLength == 0.38f)
        {
            followComponent.VerticalArmLength = 0.75f;
        }

        if (followComponent != null)
        {

            Debug.Log("Entered");


        }
    }

    private void ObjectState() {
        if (isVentObject && !hasRotated)
            {
                originalXRotation = interactablePrefab.transform.eulerAngles.x;
                newRotation = Quaternion.Euler(90, interactablePrefab.transform.eulerAngles.y, interactablePrefab.transform.eulerAngles.z);
                interactablePrefab.transform.rotation = newRotation;
                hasRotated = true;
                isVentObject = false;
                fpc.PlaySound(ventClip);  

                // Let it be open for a couple of seconds, then close
                StartCoroutine(CloseVent());
                    

                fpc.PlaySound(ventClip);

                //Set has rotated to false
            }

            // If it is not a vent object and it hasnt rotated, then it will be a whiteboard
            else
            {
                if (!hasMoved)
                {
                    Debug.Log("hasMoved is false");

                    if(op != null)
                        xMovementOffset = op.xvalue;
                    interactablePrefab.transform.position = new Vector3(
                    interactablePrefab.transform.position.x + xMovementOffset,
                    interactablePrefab.transform.position.y,
                    interactablePrefab.transform.position.z);
                    hasMoved = true;
                }
                else if (hasMoved)
                {
                    Debug.Log("hasMoved is true");
                    interactablePrefab.transform.position = new Vector3(
                        interactablePrefab.transform.position.x - xMovementOffset,
                        interactablePrefab.transform.position.y,
                        interactablePrefab.transform.position.z);
                    hasMoved = false;
                }
            }

    }
    
    private void CompareObject(Collision collision) {
        if (collision.gameObject.CompareTag("Whiteboard"))
        {
            Debug.Log("Whiteboard interacted");

            // Gets the push value of the whiteboard (Each whiteboard has different push values)
            op = collision.gameObject.GetComponent<PushValue>();
            uiInteractionText.text = "Press the interact button (E) to Move Board";
            interactablePrefab = collision.gameObject;

          
            hasCollided = true;
        }
        else if (collision.gameObject.CompareTag("Vent"))
        {
            uiInteractionText.text = "Press the interact Button (E) to Open Vent";
            interactablePrefab = collision.gameObject;
            hasCollided = true;

            // To differentiate, there is another boolean that checks if the object is a vent
            isVentObject = true;
        }

    }
    IEnumerator CloseVent()
    {
        ChangeCamera();

        // Waits for 3 seconds before closing vent
        yield return new WaitForSeconds(3);

        newRotation = Quaternion.Euler(originalXRotation, interactablePrefab.transform.eulerAngles.y, interactablePrefab.transform.eulerAngles.z);
        interactablePrefab.transform.rotation = newRotation;
        hasRotated = false;

    }

}   
