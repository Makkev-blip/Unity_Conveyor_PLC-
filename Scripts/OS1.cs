using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OS1 : MonoBehaviour
{
    public TwinCAT_Handler tcH;
   
    public bool bS1 = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        //Set the tag of this GameObject to Player
        gameObject.tag = "bS1";
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
            Debug.Log("Object Detected on bS1: " + other.name);
            bS1 = true;

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // Check other object tag
        if (other.gameObject.CompareTag("Part"))
        {
            // Object has exited bS1
            Debug.Log("Object Exited bS1: " + other.name);
            bS1 = false;

        }
    }
}
