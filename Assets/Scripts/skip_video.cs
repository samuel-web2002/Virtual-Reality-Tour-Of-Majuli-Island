using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class skip_video : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject tabvideo;
    public GameObject tab;
    public GameObject drone_vid_icon;
    public GameObject cult_icon;
    public GameObject Mapimage;
    void Start()
    {
        
    }

    public void skip()
    {
        videoPlayer.Pause();
        tabvideo.SetActive(false);
        Mapimage.SetActive(true);
        tab.SetActive(false);
        Material skyboxMaterial = RenderSettings.skybox;
        if(Cultural_icon.cult_vid.ContainsKey(skyboxMaterial.name))
        {
            cult_icon.SetActive(true);
            cult_icon.transform.GetChild(0).gameObject.SetActive(true);
        }
           
        if (Drone_icon.drone_vid.ContainsKey(skyboxMaterial.name))
        {
            drone_vid_icon.SetActive(true);
            drone_vid_icon.transform.GetChild(0).gameObject.SetActive(true);
        }

    }
}
