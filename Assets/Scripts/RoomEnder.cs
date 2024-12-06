using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEnder : MonoBehaviour, IInteractable
{
    public Vector3 playerTargetPositionInScene2; // Target position for the player in Scene 2
    public string targetSceneName; // The name of the scene to load

    private void Start()
    {
    }

    public void Interact()
    {
        StartCoroutine(WaitForInput());
    }

    private IEnumerator WaitForInput()
    {
        Debug.Log("Press 0 to cancel or 1 to swap rooms.");

        bool responded = false;

        while (!responded)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Debug.Log("0 pressed: Interaction canceled.");
                responded = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("1 pressed: Calling SwapRoom.");
                SwapRoom();
                responded = true;
            }

            yield return null; // Wait for the next frame
        }
    }

    public void SwapRoom()
    {
            // Mark the main game object to persist across scenes
            // Load the target scene asynchronously
            SceneManager.LoadSceneAsync(targetSceneName).completed += OnSceneLoaded;
    }

    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        // Find the player again in the new scene (if needed)
        // Optionally, reposition the main game object if necessary
        // mainGameObject.transform.position = someTargetPosition;
    }
}

