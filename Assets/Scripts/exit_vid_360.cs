using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class exit_vid_360 : MonoBehaviour
{
    public GameObject video_360;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void exit()
    {
        Material skyboxMaterial = icon_360_animator.previous;
        RenderSettings.skybox = skyboxMaterial;
        video_360.SetActive(false);
    }
}
