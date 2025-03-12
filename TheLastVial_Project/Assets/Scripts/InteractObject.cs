

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
        
        if (hasCollided && Input.GetButtonDown("Fire1"))
        {
            if (interactablePrefab != null)
            {
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
                else
                {
                    if (!hasMoved)
                    {
                        // fpc.PlaySound(ventClip);
                        Debug.Log("hasMoved is false");

                        if(op != null)
                            xMovementOffset = op.xvalue;
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
            isVentObject = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision exited");
        uiInteractionText.text = "";
        hasCollided = false;
        if(collision.gameObject.CompareTag("Vent")) {
            isVentObject = false;
        }
        // interactablePrefab = null;
    }



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
    IEnumerator CloseVent()
    {
        ChangeCamera();

        
        yield return new WaitForSeconds(3);

        newRotation = Quaternion.Euler(originalXRotation, interactablePrefab.transform.eulerAngles.y, interactablePrefab.transform.eulerAngles.z);
        interactablePrefab.transform.rotation = newRotation;
        hasRotated = false;

    }

}   
