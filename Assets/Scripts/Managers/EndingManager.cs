using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndingManager : Singleton<EndingManager>
{
    public VideoClip[] clips;
    public GameObject canvas;
    public VideoPlayer videoPlayer;
    public VideoClip[] winlose;
    public VideoClip[] finalResult;
    public int wlindex;

    public void ChooseEnding(int index)
    {
        wlindex = (index == 3)?0:1;
        CutsceneManager.Instance.videoPlayer.clip = clips[index];
        CutsceneManager.Instance.endScene = true;
        CutsceneManager.Instance.PlayCutscene();
        videoPlayer.loopPointReached += OnCutsceneEnd;
    }
    private void OnCutsceneEnd(VideoPlayer vp)
    {
        // Unsubscribe from the event
        vp.loopPointReached -= OnCutsceneEnd;

        // Hide the cutscene UI
        canvas.SetActive(false);
        CutsceneManager.Instance.videoPlayer.clip = winlose[wlindex];
        CutsceneManager.Instance.PlayCutscene();
        videoPlayer.loopPointReached += OnCutsceneEnd2;
    }

    private void OnCutsceneEnd2(VideoPlayer vp)
    {
        // Unsubscribe from the event
        vp.loopPointReached -= OnCutsceneEnd2;

        // Hide the cutscene UI
        canvas.SetActive(false);
        CutsceneManager.Instance.videoPlayer.clip = finalResult[wlindex];
        CutsceneManager.Instance.PlayCutscene();
        videoPlayer.loopPointReached += OnCutsceneEnd3;
    }
    private void OnCutsceneEnd3(VideoPlayer vp)
    {
        // Unsubscribe from the event
        vp.loopPointReached -= OnCutsceneEnd3;

        // Hide the cutscene UI
        canvas.SetActive(false);
        Application.Quit();
    }
}
