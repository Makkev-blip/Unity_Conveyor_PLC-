using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class testScript : MonoBehaviour
{
    
    public Vector3 fPForce = new (-0.0000005f, 0.0f, 0.0f);

    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb.AddForce(fPForce,ForceMode.Acceleration);
    }
}
