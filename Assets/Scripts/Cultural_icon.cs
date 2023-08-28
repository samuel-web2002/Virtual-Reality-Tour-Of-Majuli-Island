using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

// public class Drone_icon : MonoBehaviour
// {
//     // Duration of the animation in seconds
//     public float animationDuration = 1f;

//     // Maximum scale of the GameObject during the animation
//     public float maxScale = 0.3f;

//     // Minimum scale of the GameObject during the animation
//     public float minScale = 0.2f;

//     // Whether the animation should loop
//     public bool loopAnimation = true;

//     // Whether the animation should start playing automatically
//     public bool playAutomatically = true;

//     private Vector3 initialScale;
//     private float animationTimer=0f;
//     private bool isAnimating=true;
//     public Dictionary<string,string> drone_vid = new Dictionary<string, string>();
//     public GameObject icon;

//      private SpriteRenderer spriteRenderer;

//     // private void Awake()
//     // {
//     //     spriteRenderer = GetComponent<SpriteRenderer>();
//     // }
//     // private void OnMouseDown()
//     // {
//     //     GameObject camera = GameObject.Find("Main Camera");

//     //     var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
//     //     videoPlayer.playOnAwake = false;
//     //     videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
//     //     // videoPlayer.targetCameraAlpha = 0.5F;
//     //     videoPlayer.url = "Assets/Uttar Kamalabari (1).webm";
//     //     videoPlayer.isLooping = true;
//     //     //  videoPlayer.loopPointReached += EndReached;
//     //     videoPlayer.Play();
//     //  //   Perform the action when the sprite is clicked
//     //    Debug.Log("Sprite clicked!");
//     //    GameObject myObject = GameObject.Find("object"); // Replace "MyGameObject" with the name of your GameObject
//     //    myObject.SetActive(false);


//     //     // Change the sprite color
//     //     //spriteRenderer.color = Color.red;
//     // }

//     private void Start()
//     {
//         string activeSceneName = SceneManager.GetActiveScene().name;
//         icon.SetActive(false);
//         string filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Drone videos.txt";
//         using (StreamReader reader = new StreamReader(filePath))
//         {
//             string line;
//             while ((line = reader.ReadLine()) != null)
//             {
//                 string[] parts = line.Split('|');

//                 string key = parts[0].Trim();
//                 string value = parts[1].Trim();

//                 drone_vid.Add(key, value);
//             }
//         }
//         // Get the initial scale of the GameObject
//         initialScale = transform.localScale;
//     }

//     private void Update()
//     {
//         Material skyboxMaterial = RenderSettings.skybox;
//         if(drone_vid.ContainsKey(skyboxMaterial.name))
//         {
//             icon.SetActive(true);
//             drone_vid_icon();
//         }
//         else icon.SetActive(false);
//     }
//     public void drone_vid_icon()
//     {
//         if (isAnimating)
//         {
//             // Increment the animation timer
//             animationTimer += Time.deltaTime;

//             // Calculate the progress of the animation
//             float t = animationTimer / animationDuration;

//             // Clamp the progress to a value between 0 and 1
//             t = Mathf.Clamp01(t);

//             // Interpolate between the minimum and maximum scale based on the progress of the animation
//             float scale = Mathf.Lerp(minScale, maxScale, t);

//             // Set the scale of the GameObject
//             transform.localScale = initialScale * scale;

//             if (animationTimer >= animationDuration)
//             {
//                 if (loopAnimation)
//                 {
//                     // Restart the animation
//                     StartAnimation();
//                 }
//                 else
//                 {
//                     // Stop the animation
//                     StopAnimation();
//                 }
//             }
//         }
//     }

//     public void StartAnimation()
//     {
//         // Reset the animation timer and start animating
//         animationTimer = 0f;
//         isAnimating = true;
//     }

//     public void StopAnimation()
//     {
//         // Stop animating and reset the scale of the GameObject
//         isAnimating = false;
//         transform.localScale = initialScale;
//     }
// }
public class Cultural_icon : MonoBehaviour
{
    // Duration of the animation in seconds
   // public GameObject tabVideo;
    public static Dictionary<string,string> cult_vid = new Dictionary<string, string>();
    public GameObject cult_icon;
    public GameObject tab;

    private void Start()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        cult_icon.SetActive(false);
        string filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Cultural videos.txt";
        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');

                    string key = parts[0].Trim();
                    string value = parts[1].Trim();

                    cult_vid.Add(key, value);
                }
            }
        }
        catch (System.IO.FileNotFoundException ex)
        {
            Debug.Log("File not found: " + ex.Message);
        }
       
    }

    private void Update()
    {
        Material skyboxMaterial = RenderSettings.skybox;

        /*if (drone_vid.ContainsKey(skyboxMaterial.name)&&tabVideo.activeSelf==false&&tab.activeSelf==false)
        {
            icon.SetActive(true);
        }*/
        if (cult_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
        {
            cult_icon.SetActive(true);
        }
        else
        {
            cult_icon.SetActive(false);
        }
    }
}
