using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed;

    public int hpPlus = 0, powerPlus = 0;

    public float fuelPlus = 0f;
    
    private Rigidbody rb;

    public float power;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0, 1 * power, 0, ForceMode.Impulse);
    }

    /*
    void Update()
    {
        float distanceY = Time.deltaTime * speed;
        this.gameObject.transform.Translate(0, distanceY, 0);
    }
    */
}
