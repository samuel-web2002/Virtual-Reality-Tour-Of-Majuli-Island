    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;    
public class VIdeo_rawimage : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer.loopPointReached += OnVideoLoopPointReached;
        videoPlayer.Prepare();
    }

    public void PlayVideo()
    {
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }

    private void OnVideoLoopPointReached(VideoPlayer source)
    {
        rawImage.texture = null;
    }
}
