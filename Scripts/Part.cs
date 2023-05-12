using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Part : MonoBehaviour
{
    public TwinCAT_Handler tcHand;
   

    //Inputs from PLC code
    public int nDir;
    public float nVel;
    public float nAcc;
    public float nDec;
    public bool bExecute;
    public bool bHalt;
    

    //Outputs to PLC code
    public bool bInVelocity = false;
    
    
    //from Part
    public float currSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
       // _rb = GetComponent<Rigidbody>();
        //Set the tag of this GameObject to Player
        gameObject.tag = "Part";

        //Respawn at the set position
        transform.position= new Vector3 (0.015f, 1.262f, -5.144f);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if part is moving
        if (currSpeed > 0)
        {
            bInVelocity = true;
        }
        
        //Check to ensure current speed during acceleration doesn't go beyond the intended velocity
        if (!bHalt)
        {
            if (currSpeed >= nVel)
            {
                currSpeed = nVel;
            }
        }
        //Check to ensure current speed during deceleration doesn't get to negative
        else if (bHalt)
        {
            if (currSpeed <= nVel)
            {
                currSpeed = nVel;
            }
        }
        
        //Positive motor direction block
        if (nDir == 1 )
        {
            if (bExecute && !bHalt)
            {      //Acceleration 
                transform.Translate(Vector3.left * (currSpeed * Time.deltaTime));
                currSpeed += nAcc;
            }
            else if (bExecute && bHalt)
            {
                //Deceleration
                transform.Translate(Vector3.left * (currSpeed * Time.deltaTime));
                currSpeed -= nDec;
            }
                
        }
        
        //Negative motor direction block
        if (nDir == 0 ) 
        {
            if (bExecute && !bHalt)
            {
                transform.Translate(Vector3.right * (currSpeed * Time.deltaTime));
                currSpeed += nAcc;
            }
            else if (bHalt)
            {
                transform.Translate(Vector3.right * (currSpeed * Time.deltaTime));
                currSpeed -= nDec;
            }
        }
        
    }
}