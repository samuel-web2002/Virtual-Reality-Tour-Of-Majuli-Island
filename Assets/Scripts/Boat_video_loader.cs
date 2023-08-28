using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Boat_video_loader : MonoBehaviour
{

    // Start is called before the first frame update
    public Material material;
    public VideoPlayer videoPlayer;
    public GameObject videoloader;
    public GameObject travelskipcanvas;
    public GameObject travelskipcanvas_2;
    public GameObject starttour;
    public GameObject tab;
    void Start()
    {
        string filePath = Application.dataPath+"/Resources/Videos/Boat wait.mp4";
        //video_360.SetActive(false);
        videoPlayer.enabled = true;
       // travelskip.SetActive(true);
        videoPlayer.url = filePath;
        RenderSettings.skybox = material;
        tab.SetActive(false);
        //travelskip.SetActive(true);
        //videoPlayer.loopPointReached += (videoPlayer) => OnVideoFinished(videoPlayer);
        travelskipcanvas_2.SetActive(false);
        travelskipcanvas.SetActive(false);
        starttour.SetActive(true);
    }
    public void start_ride()
    {
        Debug.Log("started ride");
        string filePath = Application.dataPath + "/Resources/Videos/Boat travel.MP4";
        //video_360.SetActive(false);
        videoPlayer.enabled = true;
        // travelskip.SetActive(true);
        videoPlayer.url = filePath;
        //RenderSettings.skybox = material;
        tab.SetActive(false);
        // travelskip.SetActive(true);
        videoPlayer.loopPointReached += (videoPlayer) => OnVideoFinished(videoPlayer);
        travelskipcanvas_2.SetActive(false);
        starttour.SetActive(false);
        travelskipcanvas.SetActive(true);

    }
    void OnVideoFinished(VideoPlayer videoPlayer)
    {
        //VideoPlayer.Pause();
        videoloader.SetActive(false);
        travelskipcanvas.SetActive(false);
        // Debug.Log(spriteName);
        tab.SetActive(true);
        travelskipcanvas_2.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void skip_boat()
    {
        //VideoPlayer.Pause();
        videoloader.SetActive(false);
        travelskipcanvas.SetActive(false);
        // Debug.Log(spriteName);
        tab.SetActive(true);
        travelskipcanvas_2.SetActive(true);
    }

}
