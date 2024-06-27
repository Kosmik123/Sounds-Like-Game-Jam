using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CloseVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Update is called once per frame
    void Update()
    {
        if (videoPlayer.isPlaying==false)
        gameObject.SetActive(false);
        
    }
}
