using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class Sprite_control : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public float scaleFactor = 1.4f;
    public static string spriteName;
    public GameObject drone_vid_icon;
    public VideoPlayer videoPlayer;
    public GameObject video_360;
    public GameObject tab;
    public Material material;
    public GameObject travelskip;
    public Dictionary<string, int> travel = new Dictionary<string, int>();

    // Store the original scale of the sprite
    private Vector3 originalScale;
    public Vector3 enterScale = new Vector3(1.2f, 1.2f, 1.2f); // The scale to change to when the pointer enters
    public Vector3 exitScale = Vector3.one; // The scale to change to when the pointer exits

    private RectTransform buttonRectTransform;
    // Get the sprite's box collider
    private BoxCollider2D boxCollider;
    public int a; 
   //public GameObject icn;
    public Dictionary<string,GameObject> myGameObjects = new Dictionary<string,GameObject>();

   // public Text hoverText;
  

    public void Start()
    {
        // Get the original scale of the sprite
        buttonRectTransform = GetComponent<RectTransform>();
        originalScale = buttonRectTransform.localScale;

        // Get the sprite's box collider
        boxCollider = GetComponent<BoxCollider2D>();
        travel.Add("Dakhinpaat Satra+Samaguri Satra", 1);
        travel.Add("Dakhinpaat Satra+Gormur Town", 1);
        travel.Add("Dakhinpaat Satra+New Kamlabari Satra", 1);
        travel.Add("New Kamlabari Satra+Gormur Town", 1);
        travel.Add("New Kamlabari Satra+Samaguri Satra", 1);
        travel.Add("Gormur Town+Samaguri Satra", 1);
        travel.Add("Jengrai Chapori+Rongasahi gaon", 1);
        travel.Add("Jengrai Chapori+Chotaipur gaon", 1);
        travel.Add("Rongasahi gaon+Chotaipur gaon", 1);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonRectTransform.localScale = enterScale;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonRectTransform.localScale = exitScale;
    }
   public void OnMouseDown()
    {
        spriteName = gameObject.name;
        tab.SetActive(false);
        drone_vid_icon.SetActive(false);
        string activescene = SceneManager.GetActiveScene().name;
        //video_360.SetActive(false);
        if(travel.ContainsKey(activescene+"+"+spriteName) || travel.ContainsKey(spriteName + "+" + activescene))
        {
            video_360.SetActive(true);
            videoPlayer.enabled = true;
            travelskip.SetActive(true);
            // Material skyboxMaterial = RenderSettings.skybox;
            // previous = skyboxMaterial;
            // string videoname = icon_360_controller.vid_360[skyboxMaterial.name];
            Debug.Log("WHATR");
            string filePath = "Assets/Resources/Videos/Jengrai Chapori/test.mp4";
            videoPlayer.url = filePath;
            RenderSettings.skybox = material;
            travelskip.SetActive(true);
            videoPlayer.loopPointReached += (videoPlayer) => OnVideoFinished(videoPlayer, spriteName);
        }
        else
        {
            Debug.Log(spriteName);
            try
            {
                SceneManager.LoadScene(spriteName);
            }
            catch
            {
                Debug.LogError("failed to load");
            }
            Manager_scenes.NewSceneLoader();
            video_360.SetActive(false);
            travelskip.SetActive(false);
        }
       
       // 
        // Debug.Log(spriteName );
        // try
        // {
        //     SceneManager.LoadScene(spriteName);
        // }
        // catch
        // {
        //     Debug.LogError("failed to load");
        // }
        // Manager_scenes.NewSceneLoader();

        // Perform actions in response to the click here

        // Perform actions in response to the click here
    }
    void OnVideoFinished(VideoPlayer videoPlayer, string spriteName)
    {
        Debug.Log(spriteName);
        try
        {
            SceneManager.LoadScene(spriteName);
        }
        catch
        {
            Debug.LogError("failed to load");
        }
        Manager_scenes.NewSceneLoader();
        video_360.SetActive(false);
        travelskip.SetActive(false);
    }
    public void skip_travel()
    {
        //VideoPlayer.Pause();
        video_360.SetActive(false);
        travelskip.SetActive(false);
        Debug.Log(spriteName);
        try
        {
            SceneManager.LoadScene(spriteName);
        }
        catch
        {
            Debug.LogError("failed to load");
        }
        Manager_scenes.NewSceneLoader();
    }
}