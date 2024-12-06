using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using JetBrains.Annotations;

public class CutsceneManager : Singleton<CutsceneManager>
{
    public VideoPlayer videoPlayer; // Assign your Video Player
    public GameObject canvas;       // Assign your Canvas containing the Raw Image
    public string targetSceneName;    // Name of the next scene to load
    public GameObject mainGameObject; // The main game object to transfer between scenes
    public Vector3 playerTargetPositionInScene2; // Target position for the player in Scene 2
    private GameObject player;
    public bool isPlaying = false;
    private bool isSceneLoading = false;
    private bool cutsceneStarted = false;
    public bool endScene = false;


    private void Start()
    {

        PlayCutscene();
    }

    public void Update()
    {
        if(isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            skipCutscene();
        }
    }

    public void PlayCutscene()
    {
        cutsceneStarted = true; // Indicate that the cutscene has started
        canvas.SetActive(true); // Show the cutscene UI
        videoPlayer.time = 0;   // Reset video to the start
        videoPlayer.Play();
        isPlaying = true;
        Debug.Log("played");

        // Subscribe to the VideoPlayer's "loopPointReached" event
        videoPlayer.loopPointReached -= OnCutsceneEnd; // Avoid duplicate subscriptions
        videoPlayer.loopPointReached += OnCutsceneEnd;
    }


    private void OnCutsceneEnd(VideoPlayer vp)
    {
        Debug.Log($"OnCutsceneEnd called. VideoPlayer isPlaying: {videoPlayer.isPlaying}, Time: {videoPlayer.time}");
        if (!cutsceneStarted) return; // Ignore unexpected calls
        Debug.Log("OnCutsceneEnd called unexpectedly");
        cutsceneStarted = false;
        
        if (isSceneLoading) return;
        isSceneLoading = true;

        vp.loopPointReached -= OnCutsceneEnd;
        canvas.SetActive(false);
        isPlaying = false;
        Debug.Log("cutscene End");

        var asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);
        asyncOperation.completed -= OnSceneLoaded;
        asyncOperation.completed += OnSceneLoaded;
    }
    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        isSceneLoading = false; // Reset the flag after loading is complete
        // Find the player again in the new scene (if needed)
        // Find the player object in the scene (tagged as "Player")
        mainGameObject = GameObject.FindGameObjectWithTag("Main");
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found! Ensure the Player GameObject is tagged 'Player'.");
        }

        if (mainGameObject == null)
        {
            Debug.LogError("Main Game Object is not assigned!");
        }
        if (player != null)
        {
            // Move the player to the target position in the new scene
            Debug.Log("sent");
            player.transform.position = playerTargetPositionInScene2;
            player.GetComponent<PlayerMovement>().targetPosition = playerTargetPositionInScene2;
        }
        else
        {
            Debug.LogError("Player not found in the target scene!");
        }
         asyncOperation.completed -= OnSceneLoaded;
        // Optionally, reposition the main game object if necessary
        // mainGameObject.transform.position = someTargetPosition;
    }

    public void skipCutscene()
    {
        if (!cutsceneStarted || isSceneLoading) return; // Prevent redundant calls
        if(!endScene){
            isSceneLoading = true;
            cutsceneStarted = false; // Ensure cutscene state is reset
            canvas.SetActive(false);
            isPlaying = false;

            // Stop the video to avoid triggering loopPointReached
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
                videoPlayer.loopPointReached -= OnCutsceneEnd; // Unsubscribe to avoid unexpected calls
            }

            Debug.Log("Cutscene skipped.");

            // Load the next scene
            var asyncOperation = SceneManager.LoadSceneAsync(targetSceneName);
            asyncOperation.completed -= OnSceneLoaded; // Unsubscribe if already subscribed
            asyncOperation.completed += OnSceneLoaded; // Subscribe only once
        }

    }
}
