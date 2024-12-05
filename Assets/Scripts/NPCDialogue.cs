using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public string dialoguePath; // Path to Dialogue JSON file (e.g., "Dialogues/DialogueLiam")

    public void Interact()
    {
        Debug.Log("test");
        DialogueManager.Instance.InstantiateDialogue(dialoguePath);
        DialogueManager.Instance.StartDialogue();
    }
}