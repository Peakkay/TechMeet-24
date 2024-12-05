using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    //TODO
    //send dialogue directly to dialogueUXManager and make it check for input
    //an then skip next line

    public Dialogue dialogue;
    public string resourceDialogue = "Dialogues/DialogueExample";
    CharacterDatabase characterDatabase;
    public KeyCode jumpLineKey = KeyCode.Space;
    private bool dialogueShow = false;
    public bool dialogueOnDisplay;
    public void InstantiateDialogue(string resourcePath = "Dialogues/DialogueExample")
    {
        resourceDialogue = resourcePath;
        dialogue = new Dialogue(resourceDialogue);
    }
    public int NextLine()
    {
        if (dialogue != null)
        {
            dialogue.NextLine();
            if ((dialogue.currentLine > dialogue.maxLines))
            {
                dialogueShow = false;
            }
        }
        return -1;
    }
    void Start()
    {
        characterDatabase = CharacterDatabase.GetSingleton();
    }
    public void StartDialogue()
    {
        dialogueShow = true;
        NextLine();
    }
    void Update()
    {
        dialogueOnDisplay = dialogueShow;
        if (dialogueShow)
        {
            if (Input.GetKeyDown(jumpLineKey))
            {
                NextLine();
            }
        }
    }
}
