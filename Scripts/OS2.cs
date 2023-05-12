using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OS2 : MonoBehaviour
{
    public TwinCAT_Handler tcH;
    [FormerlySerializedAs("bS2")] public bool bS2 = false;
    // Start is called before the first frame update
    void Start()
    {
        //Set the tag of this GameObject to Player
        gameObject.tag = "bS2";
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
            // Object is at bS2, do something
            Debug.Log("Object Detected on bS2: " + other.name);
            bS2= true;

        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // Check other object tag
        if (other.gameObject.CompareTag("Part"))
        {
            // Object is at bS3, do something
            Debug.Log("Object Detected on bS2: " + other.name);
            bS2 = false;

        }
    }
}
