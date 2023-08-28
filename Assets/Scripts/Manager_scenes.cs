using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;


public class Manager_scenes : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<string> imagenames;
    public GameObject travelskip;
   
    public GameObject tabvideo;
    public static Dictionary<string,float> Angles;
    public static GameObject leftarrow,rightarrow,frontarrow,backarrow;
    public static float skyboxRotation = 0.0f; // Initialize the rotation to zero

     public static List<string> sitenames = new List<string> { "Dakhinpaat Satra", "New Kamlabari Satra", "Baghor gaon", "Samaguri Satra", "Chotaipur gaon", "Rongasahi gaon", "Garmur Town", "Jengrai Chapori" };
    void Start()
    {
        if(tabvideo!=null)
        tabvideo.SetActive(false); 
        if(travelskip!=null) travelskip.SetActive(false);
        GameObject prefab = Resources.Load<GameObject>("Models/Left Arrow");
        leftarrow = Instantiate(prefab);leftarrow.SetActive(false);
        prefab = Resources.Load<GameObject>("Models/Right Arrow");
        rightarrow = Instantiate(prefab);rightarrow.SetActive(false);
        prefab = Resources.Load<GameObject>("Models/Front Arrow");
        frontarrow = Instantiate(prefab);frontarrow.SetActive(false);
        prefab = Resources.Load<GameObject>("Models/Back Arrow");
        backarrow = Instantiate(prefab);backarrow.SetActive(false);
        NewSceneLoader();
    }
    public static void NewSceneLoader()
    {
        //tab.SetActive(false);
        imagenames = new List<string>();
        string activeSceneName = SceneManager.GetActiveScene().name;
        string filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Imagenames.txt";
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                imagenames.Add(line);
            }
        }
        filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Angles.txt";
        Angles = new Dictionary<string, float>();

        // Read the file and parse the data
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] tokens = line.Split('|');
                string key = tokens[0].Trim();
                float value = float.Parse(tokens[1].Trim());
                Angles.Add(key, value);
            }
        }
        else
        {
            Debug.LogError("File not found at path: " + filePath);
        }
        // Print the contents of the dictionary



        Debug.Log(imagenames.Count);
        Material skyboxMaterial = Resources.Load<Material>("Materials/"+activeSceneName+"/"+imagenames[0]);
        skyboxMaterial.SetFloat("_Rotation", Angles[skyboxMaterial.name]);
        if(SphereChanger.adjList[imagenames[0]][0]!="NIL")
        {  
          leftarrow.SetActive(true);
        }
        else leftarrow.SetActive(false);
        if(SphereChanger.adjList[imagenames[0]][1]!="NIL")
        {  
          rightarrow.SetActive(true);
        }
        else rightarrow.SetActive(false);
        if(SphereChanger.adjList[imagenames[0]][2]!="NIL")
        {  
          frontarrow.SetActive(true);
        }
        else frontarrow.SetActive(false);
        if(SphereChanger.adjList[imagenames[0]][3]!="NIL")
        {  
          backarrow.SetActive(true);
        }
        else backarrow.SetActive(false);
        ///skyboxMaterial.mainTextureOffset = new Vector2(Angles[imagenames[0]] / 360f, 0f);
        if(skyboxMaterial==null){
            Debug.Log("No starting material for scene"+imagenames[0]);
        }
        RenderSettings.skybox = skyboxMaterial;
        Debug.Log(skyboxMaterial.name);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
