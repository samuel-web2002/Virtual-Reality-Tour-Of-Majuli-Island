using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class Start_to_boat : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        videoPlayer.loopPointReached += (videoPlayer) => OnVideoFinished(videoPlayer);
    }
    void OnVideoFinished(VideoPlayer videoPlayer)
    {
        SceneManager.LoadScene("Boat ride");
    }
    public void start_to_boat()
    {
        SceneManager.LoadScene("Boat ride");
    }
}
