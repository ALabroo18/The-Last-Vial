using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI interactionText;
    public GameObject interactionPrefab;

    Quaternion targetRotation;
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
        
        interactionText.text = "hello";

        // transform.rotation = Quaternion.Euler(transform.rotation.x, 90, 0f);
        
       targetRotation = Quaternion.Euler(interactionPrefab.transform.eulerAngles.x, interactionPrefab.transform.eulerAngles.y , interactionPrefab.transform.eulerAngles.z + 90);

       interactionPrefab.transform.rotation = targetRotation;
       Debug.Log("Collided");
    }
    private void OnTriggerExit(Collider other)
    {
        interactionText.text = "";
    }

}
