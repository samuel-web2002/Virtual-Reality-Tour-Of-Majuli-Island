using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;
using UnityEngine.XR;

public class SphereChanger : MonoBehaviour
{
    //public List<GameObject> spheres;
    public float speed = 200f;int a;
    public float rotationSpeed = 2f;
   // public SceneManager sm = new SceneManager();
    public GameObject arrow;
    public Color newColor = Color.green;
    private Renderer objectRenderer;
    public GameObject icon;
    public GameObject cult_icon;
    public GameObject tab;
    // List<float> allDirections = {0,90,270,360};
    // public float raycastDistance = 10f;
    public Dictionary<string, List<int>> coordinates = new Dictionary<string, List<int>>();
     public  static Dictionary<string, List<string>> adjList;
     //public GameObject Tab;
    private float slowDistance = 0.75f; // Distance for slow part
    private float fastDistance = 1.25f; // Distance for fast part
    public float skyboxRotation=0f;
    public InputDevice rightdevice;
    public bool buttonPressed = false;
    //public InputActionProperty Righttrigger;

    //public GameObject worldCentre;
    //private int currentSphereIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        String activeSceneName = Application.loadedLevelName;
        adjList = new Dictionary<string, List<string>>();

        // Read all lines from the file
        String filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/AdjList.txt";
        string[] lines = File.ReadAllLines(filePath);

        // Loop through each line
        foreach (string line in lines)
        {
            // Split the line by spaces
            string[] parts = line.Split('|');

            // The first part is the key
            string key = parts[0].Trim();

            // The remaining parts are the values
            List<string> values = new List<string>();
            for (int i = 1; i < parts.Length; i++)
            {
                parts[i]=parts[i].Trim();
                values.Add(parts[i]);
            }

            // Add the key-value pair to the dictionary
            adjList[key] = values;
        }
        Debug.Log(adjList);
        filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Coordinates.txt";
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split('|');

                string key = parts[0].Trim();
                List<int> values = new List<int>();

                for (int i = 1; i < parts.Length; i++)
                {
                    int value;
                    parts[i]=parts[i].Trim();
                    if (int.TryParse(parts[i], out value))
                    {
                        values.Add(value);
                    }
                }

                coordinates.Add(key, values);
            }
        }
    }
    String currnode="BG1";

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        //Debug.Log(devices[0].characteristics);
        rightdevice = new InputDevice();
        if (devices.Count > 0) rightdevice = devices[0];
        string activeSceneName = SceneManager.GetActiveScene().name;
         Material skyboxMaterial = RenderSettings.skybox;
         string skyboxMaterialName = skyboxMaterial ? skyboxMaterial.name : "none";
         //Debug.Log(skyboxMaterialName);
         float angle = Camera.main.transform.rotation.eulerAngles.y;
         angle = (angle + 360) % 360;
         bool val1 = true;
        //Debug.Log(angle);
       
        if (adjList.ContainsKey(skyboxMaterialName)==false) 
        {
            Manager_scenes.leftarrow.SetActive(false);
            Manager_scenes.rightarrow.SetActive(false);
            Manager_scenes.frontarrow.SetActive(false);
            Manager_scenes.backarrow.SetActive(false);
            
            return;
        }
        if (adjList[skyboxMaterialName][0]!="NIL")
            {
                
                if(angle<=280f&&angle>=260f)
                {
                    Vector3 newScale = Manager_scenes.leftarrow.transform.localScale;
                    objectRenderer = Manager_scenes.leftarrow.GetComponent<Renderer>();
                    objectRenderer.material.color = Color.green; 
                    newScale.y = 10;
                    Manager_scenes.leftarrow.transform.localScale = newScale;
                 rightdevice.TryGetFeatureValue(CommonUsages.primaryButton, out val1);
                    
                   if(val1 && !buttonPressed)
                   {
                          buttonPressed = true;
                           skyboxMaterial = Resources.Load<Material>("Materials/" + activeSceneName + "/" + adjList[skyboxMaterialName][0]);
                            RenderSettings.skybox = skyboxMaterial;
                            skyboxMaterial.SetFloat("_Rotation", Manager_scenes.Angles[skyboxMaterial.name]);
                            skyboxMaterial = RenderSettings.skybox;
                            if (Drone_icon.drone_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
                            {
                                icon.SetActive(true);
                                icon.transform.position = new Vector3(-9.5f,0,1.6f);
                                icon.transform.eulerAngles = new Vector3(0, 90f, 0);
                            }
                            else
                            {
                                icon.SetActive(false);
                            }
                            if (Cultural_icon.cult_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
                            {
                                cult_icon.SetActive(true);
                                cult_icon.transform.position = new Vector3(-9.5f, 1.5f, 1.6f);
                                cult_icon.transform.eulerAngles = new Vector3(0, 90f, 0);
                       
                            }
                            else
                            {
                                cult_icon.SetActive(false);
                            }
                            Debug.Log(skyboxMaterial.name);
                            if (adjList[skyboxMaterial.name][0] != "NIL")
                            {
                                Manager_scenes.leftarrow.SetActive(true);
                            }
                            else Manager_scenes.leftarrow.SetActive(false);
                            if (adjList[skyboxMaterial.name][1] != "NIL")
                            {
                                Manager_scenes.rightarrow.SetActive(true);
                            }
                            else Manager_scenes.rightarrow.SetActive(false);
                            if (adjList[skyboxMaterial.name][2] != "NIL")
                            {
                                Manager_scenes.frontarrow.SetActive(true);
                            }
                            else Manager_scenes.frontarrow.SetActive(false);
                            if (adjList[skyboxMaterial.name][3] != "NIL")
                            {
                                Manager_scenes.backarrow.SetActive(true);
                            }
                            else Manager_scenes.backarrow.SetActive(false);
                    }
                else if (!val1) // Check if button is not pressed
                {
                    buttonPressed = false; // Reset button state
                }

            }
                else
                {
                    Vector3 newScale = Manager_scenes.leftarrow.transform.localScale;
                    newScale.y = 0.4f;
                    objectRenderer = Manager_scenes.leftarrow.GetComponent<Renderer>();
                    objectRenderer.material.color = Color.red;
                Manager_scenes.leftarrow.transform.localScale = newScale;

                }
            }
            if(adjList[skyboxMaterialName][1]!="NIL")
            {
                if(angle<=100f&&angle>=80f)
                {
                    Vector3 newScale = Manager_scenes.rightarrow.transform.localScale;
                    objectRenderer = Manager_scenes.rightarrow.GetComponent<Renderer>();
                    objectRenderer.material.color = Color.green;
                    newScale.y = 10;
                    Manager_scenes.rightarrow.transform.localScale = newScale;
                rightdevice.TryGetFeatureValue(CommonUsages.primaryButton, out val1);

                if (val1 && !buttonPressed)
                {
                    buttonPressed = true;
                    skyboxMaterial = Resources.Load<Material>("Materials/" + activeSceneName + "/" + adjList[skyboxMaterialName][1]);
                        skyboxMaterial.SetFloat("_Rotation", Manager_scenes.Angles[skyboxMaterial.name]);
                        RenderSettings.skybox = skyboxMaterial;
                        Debug.Log(skyboxMaterial.name);
                    skyboxMaterial = RenderSettings.skybox;
                    if (Drone_icon.drone_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
                    {
                        icon.SetActive(true);
                        icon.transform.position = new Vector3(9.5f, 0, 1.6f);
                        icon.transform.eulerAngles = new Vector3(0, 90f, 0);
                    }
                    else
                    {
                        icon.SetActive(false);
                    }
                    if (Cultural_icon.cult_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
                    {
                        cult_icon.SetActive(true);
                        cult_icon.transform.position = new Vector3(9.5f, 1.5f, 1.6f);
                        cult_icon.transform.eulerAngles = new Vector3(0, 90f, 0);
                    }
                    else
                    {
                        cult_icon.SetActive(false);
                    }
                    if (adjList[skyboxMaterial.name][0] != "NIL")
                        {
                            Manager_scenes.leftarrow.SetActive(true);
                        }
                        else Manager_scenes.leftarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][0] != "NIL")
                        {
                            Manager_scenes.leftarrow.SetActive(true);
                        }
                        else Manager_scenes.leftarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][1] != "NIL")
                        {
                            Manager_scenes.rightarrow.SetActive(true);
                        }
                        else Manager_scenes.rightarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][2] != "NIL")
                        {
                            Manager_scenes.frontarrow.SetActive(true);
                        }
                        else Manager_scenes.frontarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][3] != "NIL")
                        {
                            Manager_scenes.backarrow.SetActive(true);
                        }
                        else Manager_scenes.backarrow.SetActive(false);
                }
                else if (!val1) // Check if button is not pressed
                {
                    buttonPressed = false; // Reset button state
                }

            }
                else
                {
                    Vector3 newScale = Manager_scenes.rightarrow.transform.localScale;
                    newScale.y = 0.4f;
                    objectRenderer = Manager_scenes.rightarrow.GetComponent<Renderer>();
                    objectRenderer.material.color = Color.red;
                    Manager_scenes.rightarrow.transform.localScale = newScale;

                }
                
            }
             if(adjList[skyboxMaterialName][2]!="NIL")
            {
                
                if(angle<=10f||angle>=350f)
                {
                    Vector3 newScale = Manager_scenes.frontarrow.transform.localScale;
                    newScale.y =10;
                objectRenderer = Manager_scenes.frontarrow.GetComponent<Renderer>();
                objectRenderer.material.color = Color.green;
                Manager_scenes.frontarrow.transform.localScale = newScale;
                rightdevice.TryGetFeatureValue(CommonUsages.primaryButton, out  val1);
                if (val1 && !buttonPressed)
                {
                    buttonPressed = true;
                    skyboxMaterial = Resources.Load<Material>("Materials/" + activeSceneName + "/" + adjList[skyboxMaterialName][2]);
                        skyboxMaterial.SetFloat("_Rotation", Manager_scenes.Angles[skyboxMaterial.name]);
                        RenderSettings.skybox = skyboxMaterial;
                        Debug.Log(skyboxMaterial.name);
                    skyboxMaterial = RenderSettings.skybox;
                    if (Drone_icon.drone_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
                    {
                        icon.SetActive(true);
                        icon.transform.position = new Vector3(0f, 0f, 9.5f);
                        icon.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        icon.SetActive(false);
                    }
                    if (Cultural_icon.cult_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
                    {
                        cult_icon.SetActive(true);
                        cult_icon.transform.position = new Vector3(0f, 1.5f, 9.5f);
                        cult_icon.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        cult_icon.SetActive(false);
                    }
                    if (adjList[skyboxMaterial.name][0] != "NIL")
                        {
                            Manager_scenes.leftarrow.SetActive(true);
                        }
                        else Manager_scenes.leftarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][0] != "NIL")
                        {
                            Manager_scenes.leftarrow.SetActive(true);
                        }
                        else Manager_scenes.leftarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][1] != "NIL")
                        {
                            Manager_scenes.rightarrow.SetActive(true);
                        }
                        else Manager_scenes.rightarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][2] != "NIL")
                        {
                            Manager_scenes.frontarrow.SetActive(true);
                        }
                        else Manager_scenes.frontarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][3] != "NIL")
                        {
                            Manager_scenes.backarrow.SetActive(true);
                        }
                        else Manager_scenes.backarrow.SetActive(false);


                }
                else if (!val1) // Check if button is not pressed
                {
                    buttonPressed = false; // Reset button state
                }

            }
                else
                {
                    Vector3 newScale = Manager_scenes.frontarrow.transform.localScale;
                    newScale.y = 0.4f;
                objectRenderer = Manager_scenes.frontarrow.GetComponent<Renderer>();
                objectRenderer.material.color = Color.red;
                Manager_scenes.frontarrow.transform.localScale = newScale;

                }
                
            }
            if(adjList[skyboxMaterialName][3]!="NIL")
            {
                
                if(angle<=190f&&angle>=170f)
                {
                    Vector3 newScale = Manager_scenes.backarrow.transform.localScale;
                    newScale.y = 10;
                objectRenderer = Manager_scenes.backarrow.GetComponent<Renderer>();
                objectRenderer.material.color = Color.green;
                Manager_scenes.backarrow.transform.localScale = newScale;
                rightdevice.TryGetFeatureValue(CommonUsages.primaryButton, out val1);
                if (val1 && !buttonPressed)
                {
                    buttonPressed = true;
                        skyboxMaterial = Resources.Load<Material>("Materials/" + activeSceneName + "/" + adjList[skyboxMaterialName][3]);
                        skyboxMaterial.SetFloat("_Rotation", Manager_scenes.Angles[skyboxMaterial.name]);
                        RenderSettings.skybox = skyboxMaterial;
                        Debug.Log(skyboxMaterial.name);
                    skyboxMaterial = RenderSettings.skybox;
                    if (Drone_icon.drone_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
                    {
                        icon.SetActive(true);
                        icon.transform.position = new Vector3(0f, 0f, -9.5f);
                        icon.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        icon.SetActive(false);
                    }
                    if (Cultural_icon.cult_vid.ContainsKey(skyboxMaterial.name) && tab.activeSelf == false)
                    {
                        cult_icon.SetActive(true);
                        cult_icon.transform.position = new Vector3(0f, 1.5f, -9.5f);
                        cult_icon.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        cult_icon.SetActive(false);
                    }
                    if (adjList[skyboxMaterial.name][0] != "NIL")
                        {
                            Manager_scenes.leftarrow.SetActive(true);
                        }
                        else Manager_scenes.leftarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][0] != "NIL")
                        {
                            Manager_scenes.leftarrow.SetActive(true);
                        }
                        else Manager_scenes.leftarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][1] != "NIL")
                        {
                            Manager_scenes.rightarrow.SetActive(true);
                        }
                        else Manager_scenes.rightarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][2] != "NIL")
                        {
                            Manager_scenes.frontarrow.SetActive(true);
                        }
                        else Manager_scenes.frontarrow.SetActive(false);
                        if (adjList[skyboxMaterial.name][3] != "NIL")
                        {
                            Manager_scenes.backarrow.SetActive(true);
                        }
                        else Manager_scenes.backarrow.SetActive(false);

                }
                else if (!val1) // Check if button is not pressed
                {
                    buttonPressed = false; // Reset button state
                }

            }
                else
                {
                    Vector3 newScale = Manager_scenes.backarrow.transform.localScale;
                    newScale.y = 0.4f;
                objectRenderer = Manager_scenes.backarrow.GetComponent<Renderer>();
                objectRenderer.material.color = Color.red;
                Manager_scenes.backarrow.transform.localScale = newScale;

                }
                
            }
          //  Debug.Log(skyboxMaterial.name);
            //skyboxMaterial.SetFloat("_Rotation", skyboxRotation);

       // }
        
       
    }

    // Coroutine to move the camera to a target position
    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
            }
    }
}