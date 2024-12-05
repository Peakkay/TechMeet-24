using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText; // Assign this in the Canvas UI
    public GameObject dialogueBox; // Link this to the dialogue panel

    public static DialogueManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDialogue(string message)
    {
        dialogueBox.SetActive(true);
        dialogueText.text = message;
    }

    public void HideDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
