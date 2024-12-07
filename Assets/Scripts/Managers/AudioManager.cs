using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource bgmSource; // AudioSource for background music

    [Header("Audio Clips")]
    public AudioClip finaleScene1Clip;
    public AudioClip finaleScene2Clip;
    public AudioClip finaleScene3Clip;
    public AudioClip finaleScene4Clip;

    private Dictionary<string, AudioClip> sceneAudioClips; // Dictionary for scene-audio mapping
    private string currentSceneName;

    void Start()
    {
        DontDestroyOnLoad(gameObject); // Persist across scenes

        // Initialize the dictionary
        sceneAudioClips = new Dictionary<string, AudioClip>
        {
            { "FinaleClue", finaleScene1Clip },
            { "FinaleScene2", finaleScene2Clip },
            { "FinaleScene3", finaleScene3Clip },
            { "FinaleScene4", finaleScene4Clip }
        };
        currentSceneName = SceneManager.GetActiveScene().name;
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Play the initial scene's audio
        PlaySceneSpecificAudio();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        PlaySceneSpecificAudio();
    }

    public void PlaySceneSpecificAudio()
    {
        AudioClip clipToPlay;

        // Check if the scene has a specific clip in the dictionary
        if (sceneAudioClips.TryGetValue(currentSceneName, out clipToPlay))
        {
            // Play the clip if it is not already playing
            if (bgmSource.clip != clipToPlay)
            {
                bgmSource.clip = clipToPlay;
                bgmSource.Play();
                Debug.Log($"Playing audio for scene: {currentSceneName}");
            }
        }
        else
        {
            // Stop the audio if no specific clip is found
            Debug.LogWarning($"No audio clip for scene: {currentSceneName}. Stopping audio.");
            bgmSource.Stop();
            bgmSource.clip = null; // Clear the current clip
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
