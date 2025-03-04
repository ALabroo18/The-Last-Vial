using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI interactionText;
    public GameObject interactionPrefab;

    public int movePositionX = 0;
    public int movePositionY = 0;

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Moved");
        
        interactionText.text = "Object Moved";

        Transform original = interactionPrefab.transform;
        // Changes position by 5 if the boolean hasnt been changed
        if(isMoved == false){
            interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x + 5, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
            isMoved = true;
        }
        else if(isMoved == true) {
            interactionPrefab.transform.position = new Vector3(interactionPrefab.transform.position.x - 5, interactionPrefab.transform.position.y, interactionPrefab.transform.position.z);
            isMoved = false;
        }

    //    interactionPrefab.transform.position = targetRotation;
    //    Debug.Log("Collided");
    }
    private void OnTriggerExit(Collider other)
    {
        interactionText.text = "";
    }

}
