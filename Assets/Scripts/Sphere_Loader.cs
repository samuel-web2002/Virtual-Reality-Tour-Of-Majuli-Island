using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEditor;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

// ...
public class Sphere_Loader : Editor
{
    public static GameObject modelPrefab = Resources.Load<GameObject>("Models/InvertedNormalsSphere");
    // reference to the model prefab
    public static Shader shader = Shader.Find("Unlit/Texture");
    public static Dictionary<string, List<string>> adjList;    // public float raycastDistance = 10f;
    public static List<string> imagenames = new List<string>();
    public static string activeSceneName ="da";
    //Debug.Log(activeSceneName);
    public static TextAsset imageListTextAsset ;
    public static int num;
    public static Dictionary<string, List<int>> coordinates=new Dictionary<string, List<int>>();

   // public static Material[] material;

    public static GameObject[] modelInstance; 
    public static int str=0;
    

    //public SceneManager sm;
    // Start is called before the first frame update
       public static void DFS(string vertex,Dictionary<string, int> visited,int x,int z,string par) {
        if(vertex=="NIL") return;
        if(visited[vertex]==1) return;
        visited[vertex] = 1;
        if(par=="none")
        {
            List<int> cord = new List<int>();
            cord.Add(x);
            cord.Add(z);
            coordinates.Add(vertex,cord);
        }
        else if(adjList[par][0]==vertex)
        {
            List<int> cord = new List<int>();
            cord.Add(x);
            cord.Add(z-2);z-=2;
            coordinates.Add(vertex,cord);
        }
        else if(adjList[par][1]==vertex)
        {
            List<int> cord = new List<int>();
            cord.Add(x);
            cord.Add(z+2);z+=2;
            coordinates.Add(vertex,cord);
        }
        else if(adjList[par][2]==vertex)
        {
            List<int> cord = new List<int>();
            cord.Add(x+2);x+=2;
            cord.Add(z);
            coordinates.Add(vertex,cord);
        }
        else if(adjList[par][3]==vertex)
        {
            List<int> cord = new List<int>();
            cord.Add(x-2);x-=2;
            cord.Add(z);
            coordinates.Add(vertex,cord);
        }


        //Console.Write(vertex + " ");

        foreach (string adjacent in  adjList[vertex]) {
            if(adjacent!="NIL")
            {
                if (visited[adjacent]!=1) {
                DFS(adjacent, visited,x,z,vertex);
              }

            }
            
        }
    }

    [MenuItem("MyMenu/Create spheres")]
    public static void CreateSpheres()
    {
        //SceneManager sceneManager = new SceneManager();
        activeSceneName = EditorSceneManager.GetActiveScene().name;
        Debug.Log(activeSceneName);
        imageListTextAsset = Resources.Load<TextAsset>("Textfiles/"+activeSceneName+"/data");

        adjList = new Dictionary<string, List<string>>();int a;
        string[] lines =  imageListTextAsset.text.Split('\n');
        foreach (string line in lines)
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] values = line.Split('|');
                string imageName = values[0].Trim();
                for (int i = 0; i < values.Length; i++)
                {
                    Debug.Log(values[i]);
                }
               imagenames.Add(imageName);
                List<string> value = new List<string>();
                for(int i=1; i<values.Length; i++)
                {
                    value.Add(values[i].Trim());
                }
                // float latitude = float.Parse(values[1].Trim());
                // float longitude = float.Parse(values[2].Trim());
                // float altitude = float.Parse(values[3].Trim());
                if(adjList.ContainsKey(imageName))
                {
                    Debug.Log(imageName);
                }
                else
                adjList.Add(imageName, value);
            }
        }
        Dictionary<string, int> visited = new Dictionary<string, int>();
        foreach (string imagename in  imagenames)
        {
            visited.Add(imagename,0);
        }
        DFS(imagenames[0],visited,0,0,"none");
        WriteToFile(Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/AdjList.txt") ;
        WriteToFilecord(Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Coordinates.txt");
        num =  imagenames.Count;

       
        modelInstance = new GameObject[num]; 
        string Path = Application.dataPath + "/Resources/Images/"+activeSceneName+"/";
        for (int i = 0; i < num; i++)
        {
            string imageName =  imagenames[i];   // "image1"
            Debug.Log(imageName);
            string imagewithext = Path + imageName + ".JPG";
            if(File.Exists(imagewithext) == false)
            {
                Debug.Log("No file exists " + imageName );
                continue;
            }
            //Debug.Log("not hit\n");
            int x =  coordinates[imageName][0]; // -2
            int y =  coordinates[imageName][1]; // 0
            //int angley = int.Parse(tokens[3]);
            modelInstance[i] = Instantiate(modelPrefab, new Vector3(x, 0, y),  Quaternion.Euler(0, 0, 0));

            //     #if UNITY_EDITOR
            //     EditorCoroutineUtility.StartCoroutineOwnerless(LoadImage(filePath, texture =>
            //     {
            //         // Set the albedo texture property of the material
            //         material[i].SetTexture("_MainTex", texture);

            //         // Assign the material to a game object or renderer
                    
            //     }));
            //    #endif

           //GetComponent<Renderer>().material = material;
           //Renderer renderer = modelInstance[i].GetComponent<Renderer>();

            // set the material on the Renderer component
           // renderer.material = material;
            SphereCollider collider = modelInstance[i].AddComponent<SphereCollider>();
            modelInstance[i].gameObject.name = imageName;
            MeshRenderer meshRenderer = modelInstance[i].GetComponent<MeshRenderer>();
            SphereCollider sphereCollider = modelInstance[i].GetComponent<SphereCollider>();

        // Disable the Mesh Renderer to make the sphere invisible
               meshRenderer.enabled = false;

        // Ensure the Sphere Collider is still enabled so that it can detect collisions
                sphereCollider.enabled = true;
            //gameObject.tag += ",MainCharacter";
        }

        string filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Imagenames.txt";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write each string in the list to the file
            foreach (string str in imagenames)
            {
                writer.WriteLine(str);
            }
        }
        filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/AdjList.txt";
        WriteToFile(filePath);
        filePath = Application.dataPath+"/Resources/Textfiles/"+activeSceneName+"/Coordinates.txt";
         WriteToFilecord(filePath);

    }

    [MenuItem("MyMenu/Create materials")]
    public static void CreateMaterials()
    {
        Debug.Log(num);
        activeSceneName = EditorSceneManager.GetActiveScene().name;
        Texture[] loadedTextures = Resources.LoadAll<Texture>("Images/"+activeSceneName+"/");
        // foreach(Texture tex in loadedTextures) {
        //     Debug.Log(tex.name);
        // }

        int nopic = loadedTextures.Length;
        foreach(Texture texture in loadedTextures)
        {
            Material material = new Material(Shader.Find("Skybox/Cubemap"));
            material.SetTexture("_Tex",texture);
            AssetDatabase.CreateAsset(material,"Assets/Resources/Materials/"+activeSceneName+"/"+texture.name+".mat");
        }
        AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();
        
        //string Path = Application.dataPath + "/Images/"+activeSceneName+"/";
        //int x=loadedTextures.Count;
        //if(num<1) return;
       // material = new Material[str+10];
        //  for (int i = 0; i < Math.Min(10,num); i++)
        //  {
        //     string imageName =  imagenames[i+str]; 
        //     Material material = new Material(shader);
        //     material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        //     string filePath = Path + imageName + ".JPG";

        //     byte[] imageData = File.ReadAllBytes(filePath);
        //     Texture2D texture = new Texture2D(2, 2);
        //     texture.LoadImage(imageData);
        //     material.SetTexture("_MainTex", texture);
        //     Debug.Log(material.mainTexture);
            
            

        //     // //     // save the material as an asset in the Unity project
        //     string assetPath = "Assets/Resources/Materials/"+activeSceneName+"/"+imageName+".mat";
        //     AssetDatabase.CreateAsset(material, assetPath);
            
        //  }
        // str+=10;
        // num-=10;
    }
    // [MenuItem("MyMenu/Apply materials")]
    // public static void ApplyMaterials()
    // {
    //     for (int i = 0; i < num; i++)
    //     {
    //         Material material = Resources.Load<Material>("Materials/"+activeSceneName+"/"+imagenames[i]);
    //         if(material==null) continue;
        
    //         // Increment the material index for the next game object
    //         Renderer renderer = GameObject.Find(imagenames[i]).GetComponent<Renderer>();
    //         material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    //         renderer.material = material;

    //     }


    // }



   public static void WriteToFile(string filePath)
    {
        // Create the file if it doesn't exist
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }

        // Write the contents to the file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (KeyValuePair<string, List<string>> entry in adjList)
            {
                writer.Write(entry.Key + "|");
                foreach (string value in entry.Value)
                {
                    writer.Write(value + "|");
                }
                writer.WriteLine();
            }
        }
        // Save the file
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    public static void WriteToFilecord(string filePath)
    {
        // Create the file if it doesn't exist
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }

        // Write the contents to the file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
             foreach (var kvp in coordinates)
            {
                // Write the key to the file
                writer.Write(kvp.Key + "|");

                // Write the list of values to the file
                writer.Write(string.Join("|", kvp.Value.Select(x => x.ToString()).ToArray()));
                // Write a newline character to separate each key-value pair
                writer.WriteLine();
            }
        }
        // Save the file
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
   
     IEnumerator LoadImage(string filePath, System.Action<Texture2D> callback)
    {
        // Load the image data from the specified file path
        byte[] imageData = File.ReadAllBytes(filePath);

        // Create a new texture using the loaded data
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        // Invoke the callback with the loaded texture
        callback(texture);

        yield return null;
    }
}
