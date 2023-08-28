using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;
public class icon_animation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
     public float animationDuration = 1f;
     public GameObject tab;
    public GameObject tabvideo;
    public VideoPlayer videoPlayer;
    public GameObject drone_vid_icon;
    public GameObject cult_icon;
    public GameObject Mapimage;
    //  public RenderTexture rendertexture;

    // Maximum scale of the GameObject during the animation
    public float maxScale = 3.5f;

    // Minimum scale of the GameObject during the animation
    public float minScale = 2f;

    // Whether the animation should loop
    public bool loopAnimation = true;
    public bool isonpoint = false;

    // Whether the animation should start playing automatically
    public bool playAutomatically = true;

    private Vector3 initialScale;
    private float animationTimer;
    private bool isAnimating;
     private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        //tabvideo.SetActive(false);
        // Get the initial scale of the GameObject
        initialScale = new Vector3(0.75f, 0.75f, 0.75f); // or any other value that works for your sprite

        // Start the animation if necessary
        if (playAutomatically)
        {
            StartAnimation();
        }
    }

        private void Update()
        {
                if (isAnimating)
                {
                     if (isonpoint == true) return;
                    // Increment the animation timer
                    animationTimer += Time.deltaTime;

                    // Calculate the progress of the animation
                    float t = animationTimer / animationDuration;

                    // Clamp the progress to a value between 0 and 1
                    t = Mathf.Clamp01(t);

                    // Interpolate between the minimum and maximum scale based on the progress of the animation
                    float scale = Mathf.Lerp(minScale, maxScale, t);

                    // Set the scale of the GameObject
                    transform.localScale = initialScale * scale;

                    if (animationTimer >= animationDuration)
                    {
                        if (loopAnimation)
                        {
                            // Restart the animation
                            StartAnimation();
                        }
                        else
                        {
                            // Stop the animation
                            StopAnimation();
                        }
                    }
                }
            
            else
            {
                StopAnimation();
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
             isonpoint = true;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            isonpoint = false;
        }

    public void StartAnimation()
    {
        // Reset the animation timer and start animating
        animationTimer = 0f;
        isAnimating = true;
    }

        public void StopAnimation()
        {
            // Stop animating and reset the scale of the GameObject
            isAnimating = false;
            transform.localScale = initialScale;
        }
        public void OnMouseDown()
        {
            tabvideo.SetActive(true);
            tab.SetActive(true);
            drone_vid_icon.SetActive(false);
            Material skyboxMaterial = RenderSettings.skybox;
            string videoname = Drone_icon.drone_vid[skyboxMaterial.name];
            string filePath = "Assets/Resources/Videos/"+SceneManager.GetActiveScene().name+"/"+videoname+".mp4";
            videoPlayer.url= filePath;
            // render texture sizes;
            Mapimage.SetActive(false);
            Debug.Log("Sprite clicked!");
        }
        public void cult_vid_play()
        {
            tabvideo.SetActive(true);
            tab.SetActive(true);
            cult_icon.SetActive(false);
            Material skyboxMaterial = RenderSettings.skybox;
            string videoname = Cultural_icon.cult_vid[skyboxMaterial.name];
            string filePath = "Assets/Resources/Videos/" + SceneManager.GetActiveScene().name + "/" + videoname + ".mp4";
            videoPlayer.url = filePath;
            // render texture sizes;
            Mapimage.SetActive(false);
            Debug.Log("Sprite clicked!");
        }

}
