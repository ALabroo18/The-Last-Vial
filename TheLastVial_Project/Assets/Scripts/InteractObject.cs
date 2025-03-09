using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public GameObject interactionPrefab;


    public float movePositionX = 3;
    public int movePositionY = 0;


    // Bools to check collision and if moved
    private bool isCollision = false;

    private bool isMoved = false;
    private bool isRotated = false;


    //Vent Interaction
    [SerializeField] private bool isVent = false;
    Quaternion targetRotation;

    // Saves rotation of x axis before it gets rotated
    float rotationX;

    // Death Screen
    MainMenu mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollision && Input.GetKeyDown(KeyCode.E)) {

            
            if(interactionPrefab != null) {

                if(isVent == true && isRotated == false) {
                    rotationX = interactionPrefab.transform.eulerAngles.x;
                    targetRotation = Quaternion.Euler(90, interactionPrefab.transform.eulerAngles.y , interactionPrefab.transform.eulerAngles.z);
                    interactionPrefab.transform.rotation = targetRotation;
                    isRotated = true;

                }
                else if(isVent == true && isRotated == true) {
                    targetRotation = Quaternion.Euler(rotationX, interactionPrefab.transform.eulerAngles.y , interactionPrefab.transform.eulerAngles.z);
                    interactionPrefab.transform.rotation = targetRotation;
                    isRotated = false;
                }
                else {
                    if(isMoved == false) {
                     Debug.Log("false");
                     interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x + movePositionX, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
                     isMoved = true;
                    }
                    else if(isMoved == true) {
                        Debug.Log("true");
                        interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x - movePositionX, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
                        isMoved = false;
                    }
                }

                
                   

                

            }

        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Whiteboard")) {
            Debug.Log("Moved");
            interactionText.text = "Press E to Move Board";
            interactionPrefab = other.gameObject;
            isCollision = true;
        }
        else if(other.gameObject.CompareTag("Vent")) {
            interactionText.text = "Press E to Open/Close Vent";
            interactionPrefab = other.gameObject;
            isCollision = true;
            isVent = true;
        }
        
        

        // Transform original = interactionPrefab.transform;
        // // Changes position by 5 if the boolean hasnt been changed
        // if(isMoved == false){
        //     interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x + 5, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
        //     isMoved = true;
        // }
        // else if(isMoved == true) {
            // interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x - 5, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
        //     isMoved = false;
        // }

    //    interactionPrefab.transform.position = targetRotation;
    //    Debug.Log("Collided");
    }
    private void OnCollisionExit(Collision other)
    {
        Debug.Log("exit");
        interactionText.text = "";
        isCollision = false;
    }

    

}
