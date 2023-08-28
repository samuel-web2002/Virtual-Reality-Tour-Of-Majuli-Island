using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class tester : MonoBehaviour
{
    public InputDevice rightdevice;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
       // Debug.Log(devices[0].characteristics);
        rightdevice = new InputDevice();
        if (devices.Count > 0) rightdevice = devices[0];
        rightdevice.SendHapticImpulse(0, 0.9f, 5f);
        Debug.Log("haptics sent\n");
    }
}
