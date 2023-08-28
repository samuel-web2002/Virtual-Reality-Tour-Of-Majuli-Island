using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Material skyboxMaterial = Resources.Load<Material>("Materials/Jengrai Chapori/R0010158");
        RenderSettings.skybox = skyboxMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
