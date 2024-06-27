using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CloseVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private bool wasPlaying;

	void Update()
    {
        if (videoPlayer.isPlaying)
            wasPlaying = true;

        if (wasPlaying && videoPlayer.isPlaying==false)
            gameObject.SetActive(false);
    }
}
