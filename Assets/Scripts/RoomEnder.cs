using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEnder : MonoBehaviour, IInteractable
{
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

        DialogueUXManager.instance.ShowBox();
        DialogueUXManager.instance.UpdateDialogue("Choice", "Press 0 to cancel or 1 to swap rooms.", "#ffffff", null);
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

