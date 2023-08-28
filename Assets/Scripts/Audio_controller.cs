using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Audio_controller : MonoBehaviour
{
    public static Dictionary<string, string> audios = new Dictionary<string, string>();
    public AudioSource audioSource;
    public bool shouldPlay = true;
    private string audioUrl = "https://example.com/audio.mp3";
    public InputDevice leftdevice;
    public bool val = false;
    public bool buttonPressed = false;
    public bool audioon = true;

    private void Start()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;
        string filePath = Application.dataPath + "/Resources/Textfiles/" + activeSceneName + "/Audios.txt";
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

                    audios.Add(key, value);
                }
            }
        }
        catch (System.IO.FileNotFoundException ex)
        {
            Debug.Log("File not found: " + ex.Message);
        }

    }
    public void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        //Debug.Log(devices[0].characteristics);
        leftdevice = new InputDevice();
        if (devices.Count > 0) leftdevice = devices[0];
        Material skyboxMaterial = RenderSettings.skybox;
        string skyboxMaterialname = skyboxMaterial.name;

        leftdevice.TryGetFeatureValue(CommonUsages.primaryButton, out val);

        if (val && !buttonPressed)
        {
            buttonPressed = true; audioon = !audioon;
        }
        else if (!val) // Check if button is not pressed
        {
            buttonPressed = false; // Reset button state
        }
        if (audios.ContainsKey(skyboxMaterialname))
        {
            shouldPlay = true; 
            audioUrl =Application.dataPath+"/Resources/Audios/" + SceneManager.GetActiveScene().name + "/" + audios[skyboxMaterialname]+".mp3";
        }
        else shouldPlay = false;
        if (shouldPlay && audioon)
        {
            if (!audioSource.isPlaying || (audioSource.clip.name!=audioUrl))
            {
                // If the audio source is not currently playing, load the clip from the URL and start playing from the beginning
                AudioClip clip = LoadAudioClipFromFile(audioUrl);
                audioSource.clip = clip;
                audioSource.Play();
            }
            else if (audioSource.clip != null && audioSource.clip.name == audioUrl)
            {
                // If the audio source is playing the correct clip, resume playback from the current position
                audioSource.UnPause();
            }
        }
        else if (audioSource.isPlaying)
        {
            // If the audio source is playing but shouldPlay is false, pause playback
            audioSource.Pause();
        }
        audioSource.volume = (0.7f);
    }
    AudioClip LoadAudioClipFromFile(string filePath)
    {
        string fullPath = Path.Combine(Application.dataPath, filePath);
        if (!File.Exists(fullPath))
        {
            Debug.Log("Failed to load audio clip: " + fullPath + " does not exist");
            return null;
        }
        else
        {
            WWW www = new WWW("file://" + fullPath);
            while (!www.isDone) { } // Wait for file to load
            AudioClip clip = www.GetAudioClip(false);
            clip.name = filePath;
            return clip;
        }
    }
}