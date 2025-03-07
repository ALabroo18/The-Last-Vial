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
    Quaternion targetRotation;
    Transform targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollision && Input.GetKey(KeyCode.E)) {

            // Move object
            Debug.Log("If statement works");
            // interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x - 5, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);

            if(interactionPrefab != null) {

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

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Whiteboard")) {
            Debug.Log("Moved");
            interactionText.text = "Object Moved";
            interactionPrefab = other.gameObject;
            isCollision = true;
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
