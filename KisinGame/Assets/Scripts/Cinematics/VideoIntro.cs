using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoIntro : MonoBehaviour
{
    VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //videoPlayer.targetCamera = Camera.main;
        videoPlayer.waitForFirstFrame = true;
        videoPlayer.started += VideoStarted;
    }

    void VideoStarted(VideoPlayer vp)
    {
        StartCoroutine(CheckForVideoStoppedPlaying());
    }

    IEnumerator CheckForVideoStoppedPlaying()
    {
        while (videoPlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(5);
    }
}
