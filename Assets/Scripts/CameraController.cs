using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CameraController : MonoBehaviour
{
    //sensitivity of mouse
    private float rotationSpeed = 300.0f;

    public List<string> imagenames = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed, transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed, 0);
        }
    }
}