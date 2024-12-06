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
        canvas.SetActive(true); // Show the cutscene UI
        videoPlayer.Play();
        isPlaying = true;

        // Subscribe to the VideoPlayer's "loopPointReached" event
        videoPlayer.loopPointReached += OnCutsceneEnd;
    }

    private void OnCutsceneEnd(VideoPlayer vp)
    {
        // Unsubscribe from the event
        vp.loopPointReached -= OnCutsceneEnd;

        // Hide the cutscene UI
        canvas.SetActive(false);
        isPlaying = false;

        // Load the next scene
        SceneManager.LoadSceneAsync(targetSceneName).completed += OnSceneLoaded;
    }
    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
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
            player.transform.position = playerTargetPositionInScene2;
            player.GetComponent<PlayerMovement>().targetPosition = playerTargetPositionInScene2;
        }
        else
        {
            Debug.LogError("Player not found in the target scene!");
        }

        // Optionally, reposition the main game object if necessary
        // mainGameObject.transform.position = someTargetPosition;
    }

    public void skipCutscene()
    {
        canvas.SetActive(false);
        isPlaying = false;

        // Load the next scene
        SceneManager.LoadSceneAsync(targetSceneName).completed += OnSceneLoaded;
    }
}
