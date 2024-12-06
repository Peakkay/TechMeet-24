using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class RoomSwapper : MonoBehaviour, IInteractable
{
    public GameObject mainGameObject; // The main game object to transfer between scenes
    public Vector3 playerTargetPositionInScene2; // Target position for the player in Scene 2
    public string targetSceneName; // The name of the scene to load
    private GameObject player;
    public VideoClip Clip;

    private void Start()
    {
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
    }

    public void Interact()
    {
        StartCoroutine(WaitForInput());
    }

    private IEnumerator WaitForInput()
    {
        DialogueUXManager.instance.ShowBox();
        DialogueUXManager.instance.UpdateDialogue("Choice", "Press 0 to cancel or 1 to swap rooms.", "#ffffff", null);
        Debug.Log("Press 0 to cancel or 1 to swap rooms.");

        bool responded = false;

        while (!responded)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                DialogueUXManager.instance.HideBox();
                Debug.Log("0 pressed: Interaction canceled.");
                responded = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DialogueUXManager.instance.HideBox();
                Debug.Log("1 pressed: Calling SwapRoom.");
                SwapRoom();
                responded = true;
            }

            yield return null; // Wait for the next frame
        }
    }

    public void SwapRoom()
    {
        if (player != null && mainGameObject != null)
        {
            // Mark the main game object to persist across scenes
            DontDestroyOnLoad(mainGameObject);
            CutsceneManager.Instance.targetSceneName = targetSceneName;
            CutsceneManager.Instance.playerTargetPositionInScene2 =playerTargetPositionInScene2;
            CutsceneManager.Instance.videoPlayer.clip = Clip;
            CutsceneManager.Instance.PlayCutscene();
        }
    }

}
