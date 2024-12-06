using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public string dialoguePath;  // Path to Dialogue JSON file (e.g., "Dialogues/DialogueLiam")
    public Clue associatedClue;  // The clue associated with this NPC

    public void Interact()
    {
        Debug.Log("Interacted with NPC");

        // Show the NPC's dialogue
        DialogueManager.Instance.InstantiateDialogue(dialoguePath);
        DialogueManager.Instance.StartDialogue();

        // Discover the clue associated with this NPC without showing the dialogue box
        if (associatedClue != null)
        {
            ClueManager.Instance.DiscoverClueFromNPC(associatedClue);  // Discover the clue
            Debug.Log($"Clue '{associatedClue.clueName}' discovered by interacting with NPC.");
        }
        else
        {
            Debug.LogWarning("No associated clue found for this NPC.");
        }
    }
}
