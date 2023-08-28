using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class playpause : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Button button;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private bool isPlaying = true;

    void Start()
    {
        button.image.sprite = pauseSprite;
    }

    public void TogglePlayPause()
    {
        if (isPlaying)
        {
            videoPlayer.Pause();
            button.image.sprite = playSprite;
            isPlaying = false;
        }
        else
        {
            videoPlayer.Play();
            button.image.sprite = pauseSprite;
            isPlaying = true;
        }
    }
}