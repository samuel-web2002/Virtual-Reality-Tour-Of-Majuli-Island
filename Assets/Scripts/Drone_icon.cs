using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
public class Drone_icon : MonoBehaviour
{
    // Duration of the animation in seconds
    // public GameObject tabVideo;
    public static Dictionary<string, string> drone_vid = new Dictionary<string, string>();
    public GameObject icon;
    public GameObject tab;

    private void Start()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        icon.SetActive(false);
        string filePath = Application.dataPath + "/Resources/Textfiles/" + activeSceneName + "/Drone videos.txt";
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

                    drone_vid.Add(key, value);
                }
            }
        }
        catch (System.IO.FileNotFoundException ex)
        {
            Debug.Log("File not found: " + ex.Message);
        }

    }
}