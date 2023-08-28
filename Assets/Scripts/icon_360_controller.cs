using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class icon_360_controller : MonoBehaviour
{
     // Duration of the animation in seconds
    public static Dictionary<string,string> vid_360 = new Dictionary<string, string>();
    public GameObject icon_360;
    public GameObject tabVideo;
    public GameObject tab;


    private void Start()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        icon_360.SetActive(false);
        string filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Videos 360.txt";
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                vid_360.Add(key, value);
            }
        }
       
    }

    private void Update()
    {
        Material skyboxMaterial = RenderSettings.skybox;

        if (vid_360.ContainsKey(skyboxMaterial.name)&&tabVideo.activeSelf==false&&tab.activeSelf==false)
        {
            icon_360.SetActive(true);
        }
        else
        {
            icon_360.SetActive(false);
        }
    }
}
