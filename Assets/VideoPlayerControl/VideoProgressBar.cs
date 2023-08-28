using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.XR;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VideoProgressBar : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private Image progress;

    private void Awake()
    {
        progress = GetComponent<Image>();
    }

    private void Update()
    {
        if (videoPlayer.frameCount > 0)
            progress.fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
    }

    public void OnDrag(PointerEventData eventData)
    {
        TrySkip(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TrySkip(eventData);
    }

    private void TrySkip(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            progress.rectTransform, eventData.position, Camera.main, out localPoint))
        {
            float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
            SkipToPercent(pct);
        }
    }

    private void SkipToPercent(float pct)
    {
        var frame = videoPlayer.frameCount * pct;
        videoPlayer.frame = (long)frame;
    }
}
/*public class VideoProgressBar : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    public InputDevice leftdevice;
    public bool buttonPressed = false;
    public bool val = false;


    *//*[SerializeField]
    private OVRInput.Button skipButton = OVRInput.Button.PrimaryIndexTrigger;*//*

    private Image progress;

    private void Awake()
    {
        progress = GetComponent<Image>();
    }

    private void Update()
    {
        if (videoPlayer.frameCount > 0)
            progress.fillAmount = (float)videoPlayer.frame / (float)videoPlayer.frameCount;

        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        //Debug.Log(devices[0].characteristics);
        leftdevice = new InputDevice();
        *//* foreach(InputDevice device in devices)
         {
             Debug.Log(device.characteristics);
         }*//*
        if (devices.Count > 0) leftdevice = devices[0];
        leftdevice.TryGetFeatureValue(CommonUsages.primaryButton, out val);

        *//*if (val && !buttonPressed)
        {
            buttonPressed = true;
            TrySkip();

        }
        else if (!val) // Check if button is not pressed
        {
            buttonPressed = false; // Reset button state
        }*/
        /*if (OVRInput.GetDown(skipButton))
        {
            TrySkip();
        }*//*
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TrySkip();
    }

    *//*    private void TrySkip()
        {
            List<XRNodeState> nodeStates = new List<XRNodeState>();
            InputTracking.GetNodeStates(nodeStates);

            foreach (XRNodeState nodeState in nodeStates)
            {
                if (nodeState.nodeType == XRNode.LeftHand && nodeState.TryGetPosition(out Vector3 position))
                {
                    if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
                        progress.rectTransform, position, null, out Vector3 localPoint))
                    {
                        float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
                        SkipToPercent(pct);
                    }
                }
            }

            *//*if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            progress.rectTransform, InputTracking.GetLocalPosition(XRNode.LeftHand), null, out localPoint))
            {
                float pct = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
                SkipToPercent(pct);
            }*//*
        }*//*
    private void TrySkip()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == progress.gameObject)
            {
                Vector2 localPoint;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    progress.rectTransform, result.screenPosition, null, out localPoint))
                {
                    float normalizedX = Mathf.InverseLerp(progress.rectTransform.rect.xMin, progress.rectTransform.rect.xMax, localPoint.x);
                    SkipToPercent(normalizedX);
                    break;
                }
            }
        }
    }

    private void SkipToPercent(float pct)
    {
        pct = Mathf.Clamp01(pct); // Ensure pct is within 0-1 range

        var frame = Mathf.RoundToInt(pct * (float)videoPlayer.frameCount);
        videoPlayer.frame = frame;
    }

}*/