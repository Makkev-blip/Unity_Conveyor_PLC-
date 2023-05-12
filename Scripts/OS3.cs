using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OS3 : MonoBehaviour
{
    public TwinCAT_Handler tcH;
    
    public bool bS3 = false;
    // Start is called before the first frame update
    void Start()
    {
        //Set the tag of this GameObject to Player
        gameObject.tag = "bS3";
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check other object tag
        if (other.gameObject.CompareTag("Part"))
        {
            // Object is at bS1, do something
            Debug.Log("Object Detected on bS3: " + other.name);
            bS3 = true;

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // Check other object tag
        if (other.gameObject.CompareTag("Part"))
        {
            // Object is at bS3, do something
            Debug.Log("Object Detected on bS3: " + other.name);
            bS3 = false;

        }
    }
}
