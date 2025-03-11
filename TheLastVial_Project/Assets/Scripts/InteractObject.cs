// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;

// public class InteractObject : MonoBehaviour
// {
//     public TextMeshProUGUI interactionText;
//     public GameObject interactionPrefab;


//     public float movePositionX = 3;
//     public int movePositionY = 0;


//     // Bools to check collision and if moved
//     private bool isCollision = false;

//     private bool isMoved = false;
//     private bool isRotated = false;


//     //Vent Interaction
//     [SerializeField] private bool isVent = false;
//     Quaternion targetRotation;

//     // Saves rotation of x axis before it gets rotated
//     float rotationX;

//     // Death Screen
//     MainMenu mainMenu;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(isCollision && Input.GetKeyDown(KeyCode.E)) {

            
//             if(interactionPrefab != null) {

//                 if(isVent == true && isRotated == false) {
//                     rotationX = interactionPrefab.transform.eulerAngles.x;
//                     targetRotation = Quaternion.Euler(90, interactionPrefab.transform.eulerAngles.y , interactionPrefab.transform.eulerAngles.z);
//                     interactionPrefab.transform.rotation = targetRotation;
//                     isRotated = true;

//                 }
//                 else if(isVent == true && isRotated == true) {
//                     targetRotation = Quaternion.Euler(rotationX, interactionPrefab.transform.eulerAngles.y , interactionPrefab.transform.eulerAngles.z);
//                     interactionPrefab.transform.rotation = targetRotation;
//                     isRotated = false;
//                 }
//                 else {
//                     if(isMoved == false) {
//                      Debug.Log("false");
//                      interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x + movePositionX, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
//                      isMoved = true;
//                     }
//                     else if(isMoved == true) {
//                         Debug.Log("true");
//                         interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x - movePositionX, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
//                         isMoved = false;
//                     }
//                 }

                
                   

                

//             }

//         }
        
//     }

//     private void OnCollisionEnter(Collision other)
//     {
//         if(other.gameObject.CompareTag("Whiteboard")) {
//             Debug.Log("Moved");
//             interactionText.text = "Press E to Move Board";
//             interactionPrefab = other.gameObject;
//             isCollision = true;
//         }
//         else if(other.gameObject.CompareTag("Vent")) {
//             interactionText.text = "Press E to Open/Close Vent";
//             interactionPrefab = other.gameObject;
//             isCollision = true;
//             isVent = true;
//         }
        
        

//         // Transform original = interactionPrefab.transform;
//         // // Changes position by 5 if the boolean hasnt been changed
//         // if(isMoved == false){
//         //     interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x + 5, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
//         //     isMoved = true;
//         // }
//         // else if(isMoved == true) {
//             // interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x - 5, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
//         //     isMoved = false;
//         // }

//     //    interactionPrefab.transform.position = targetRotation;
//     //    Debug.Log("Collided");
//     }
//     private void OnCollisionExit(Collision other)
//     {
//         Debug.Log("exit");
//         interactionText.text = "";
//         isCollision = false;
//     }

    

// }

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
    public GameObject interactablePrefab;

    public CinemachineVirtualCamera myCamera;

    public float xMovementOffset = 3;
    public int yMovementOffset = 0;

    // Audio Source
    [SerializeField] private AudioSource ventSoundSource;

    // Bools to check collision and state
    private bool hasCollided = false;
    private bool hasMoved = false;
    private bool hasRotated = false;

    private bool isCooldown = false;

    // Vent Interaction
    [SerializeField] private bool isVentObject = false;
    Quaternion newRotation;

    // Saves rotation of x-axis before it gets rotated
    float originalXRotation;

    // Death Screen
    MainMenu gameMainMenu;
    public AudioClip ventClip;

    public FirstPersonController fpc;

    // public Cinemachine cineamachine


    // Bool to check if play is inside vent

    private bool isIn = false;

    // Start is called before the first frame update
    void Start()
    {
        fpc = this.GetComponent<FirstPersonController>();
        // Ensure the vent sound does not play when the game starts
        // if (ventSoundSource != null && ventSoundSource.isPlaying)
        // {
        //     ventSoundSource.Stop();
        // }
    }

    // Update is called once per frame
    void Update()
    {
        // Opening Vents and whiteoards
        if (hasCollided && Input.GetButtonDown("Fire1"))
        {
            if (interactablePrefab != null)
            {
                // If player is interacting with Vent
                if (isVentObject && !hasRotated && !isCooldown)
                {
                    originalXRotation = interactablePrefab.transform.eulerAngles.x;
                    newRotation = Quaternion.Euler(90, interactablePrefab.transform.eulerAngles.y, interactablePrefab.transform.eulerAngles.z);
                    interactablePrefab.transform.rotation = newRotation;
                    hasRotated = true;

                    fpc.PlaySound(ventClip);

                    //Turn off 
                }
                else if (isVentObject && hasRotated)
                {
                    newRotation = Quaternion.Euler(originalXRotation, interactablePrefab.transform.eulerAngles.y, interactablePrefab.transform.eulerAngles.z);
                    interactablePrefab.transform.rotation = newRotation;
                    hasRotated = false;
                    isCooldown = true;

                    // Play vent sound
                    fpc.PlaySound(ventClip);
                }
                else
                {
                    if (!hasMoved)
                    {
                        // fpc.PlaySound(ventClip);
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
        // fpc.PlaySound(ventClip);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Whiteboard"))
        {
            Debug.Log("Whiteboard interacted");
            uiInteractionText.text = "Press E/A to Move Board";
            interactablePrefab = collision.gameObject;
            hasCollided = true;
        }
        else if (collision.gameObject.CompareTag("Vent"))
        {
            uiInteractionText.text = "Press E/A to Open/Close Vent";
            interactablePrefab = collision.gameObject;
            hasCollided = true;
            isVentObject = true;

            // Assign vent's AudioSource if not already assigned
            // if (ventSoundSource == null)
            // {
            //     ventSoundSource = interactablePrefab.GetComponent<AudioSource>();
            // }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision exited");
        uiInteractionText.text = "";
        hasCollided = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Vent in") && isIn == false) {

            Debug.Log("hi");
            // save original cinemachine virtual camera into a different variable
            Cinemachine3rdPersonFollow followComponent = myCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

            //If the cinemachine is recognized
            if(followComponent != null) {

                 Debug.Log("Entered");
                 followComponent.VerticalArmLength = 0.38f;
                //Change bool to true
                 isIn = true;

                 // Turn off collision for two seconds

                //  StartCoroutine(CooldownRoutine(other.GetComponent<Collider>()));
            }

            // Change Rotation back

            newRotation = Quaternion.Euler(originalXRotation, interactablePrefab.transform.eulerAngles.y, interactablePrefab.transform.eulerAngles.z);
            interactablePrefab.transform.rotation = newRotation;
            hasRotated = false;
            isCooldown = true;
            fpc.PlaySound(ventClip);

            // Disable the in trigger and enable the out trigger 

            // change the cinemachine camera
           

            
        }
        if(other.gameObject.CompareTag("Vent out") && isIn == true) {
            //change the cinemachine camera back to original 
             Cinemachine3rdPersonFollow followComponent = myCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

             if (followComponent !=  null) {
                // change the cinemachine camera
                followComponent.VerticalArmLength = 0.75f;

                Debug.Log("Exit");

                //change bool to false
                isIn = false;

                // Disable the out trigger and enable the in trigger
             }

            
        }
        
    }

    IEnumerator CooldownRoutine(Collider trigger)
    {

        trigger.GetComponent<BoxCollider>().enabled = false;

        
        // Wait for the cooldown time
        yield return new WaitForSeconds(2);

        trigger.GetComponent<BoxCollider>().enabled = true;

        isCooldown = false;
    }
}   
