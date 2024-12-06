using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSwapper : MonoBehaviour, IInteractable
{
    public GameObject mainGameObject; // The main game object to transfer between scenes
    public Vector3 playerTargetPositionInScene2; // Target position for the player in Scene 2
    public string targetSceneName; // The name of the scene to load
    private GameObject player;

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

            // Load the target scene asynchronously
            SceneManager.LoadSceneAsync(targetSceneName).completed += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        // Find the player again in the new scene (if needed)
        player = GameObject.FindGameObjectWithTag("Player");

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
}
