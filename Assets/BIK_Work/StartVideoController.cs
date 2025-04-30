using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartVideoController : MonoBehaviour {
    private VideoPlayer videoPlayer;

    private void Awake() {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp) {
        Debug.LogWarning("Video ended, loading main scene.");
        SceneManager.Inst.LoadMainScene();
    }
}
