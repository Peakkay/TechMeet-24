using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public string dialoguePath; // Path to Dialogue JSON file (e.g., "Dialogues/DialogueLiam")
    public Clue associatedClue; // Clue to be discovered upon dialogue completion

    private bool isDialogueActive = false; // Tracks if dialogue is currently active

    public void Interact()
    {
        if (!isDialogueActive)
        {
            Debug.Log("Starting NPC dialogue...");
            DialogueManager.Instance.InstantiateDialogue(dialoguePath);
            DialogueManager.Instance.StartDialogue();
            isDialogueActive = true;

            // Start monitoring for dialogue completion
            StartCoroutine(WaitForDialogueCompletion());
        }
    }

    private IEnumerator WaitForDialogueCompletion()
    {
        // Wait until the dialogue finishes
        while (DialogueManager.Instance.dialogue != null && DialogueManager.Instance.dialogueOnDisplay)
        {
            yield return null;
        }

        // Once dialogue is complete, discover the associated clue
        if (associatedClue != null && !associatedClue.isDiscovered)
        {
            ClueManager.Instance.DiscoverClue(associatedClue);
            Debug.Log($"Discovered clue from NPC: {associatedClue.clueName}");
        }

        // Reset dialogue activity status
        isDialogueActive = false;
    }
}
