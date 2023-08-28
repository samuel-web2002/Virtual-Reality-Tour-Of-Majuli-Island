using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Tab_controller : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    public GameObject objectToToggle;
    public bool val = true;
    public InputDevice rightdevice;
    public bool buttonPressed = false;

    // Boolean flag to track the active state of the object
    private bool isActive = false;

    void Start()
    {
        objectToToggle.SetActive(false);
    }

    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        //Debug.Log(devices[0].characteristics);
        rightdevice = new InputDevice();
        if (devices.Count > 0) rightdevice = devices[0];
        rightdevice.TryGetFeatureValue(CommonUsages.secondaryButton, out val);

        if (val && !buttonPressed)
        {
            isActive = !isActive;
            objectToToggle.SetActive(isActive);
        }
        else if (!val) // Check if button is not pressed
        {
            buttonPressed = false; // Reset button state
        }
        // Check if the T key is pressed
       // if (Input.GetKeyDown(KeyCode.T))
       // {
            // Toggle the active state of the object
        //    isActive = !isActive;
         //   objectToToggle.SetActive(isActive);
      //  }
    }
}