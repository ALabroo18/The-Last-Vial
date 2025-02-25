using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI interactionText;
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
        Debug.Log("Collided");
        interactionText.text = "hello";

        this.transform.Rotate(0.0f, 90.0f, 0.0f, Space.World);


    }
    private void OnTriggerExit(Collider other)
    {
        interactionText.text = "";
    }

}
