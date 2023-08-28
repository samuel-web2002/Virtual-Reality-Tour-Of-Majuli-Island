using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class icon_360_animator : MonoBehaviour
{
    public GameObject drone_vid_icon;
    public GameObject vid_360_icon;
    public VideoPlayer videoPlayer;
    public GameObject video_360;
    // Start is called before the first frame update
     public float animationDuration = 1f;
    public Material material;
    public static Material previous;

    // Maximum scale of the GameObject during the animation
    public float maxScale = 1f;

    // Minimum scale of the GameObject during the animation
    public float minScale = 0.5f;

    // Whether the animation should loop
    public bool loopAnimation = true;

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
        // Get the initial scale of the GameObject
        initialScale = new Vector3(0.3f, 0.3f, 0.3f); // or any other value that works for your sprite

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
    private void OnMouseDown()
    {
        drone_vid_icon.SetActive(false);
        vid_360_icon.SetActive(false);
        video_360.SetActive(true);
        videoPlayer.enabled =true;
        Material skyboxMaterial = RenderSettings.skybox;
        previous = skyboxMaterial;
        string videoname = icon_360_controller.vid_360[skyboxMaterial.name];
        string filePath = "Assets/Resources/Videos/"+SceneManager.GetActiveScene().name+"/"+videoname+".mp4";
        videoPlayer.url= filePath;
        RenderSettings.skybox = material;
    }
}
