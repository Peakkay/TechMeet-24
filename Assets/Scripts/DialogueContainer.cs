using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueContainer", menuName = "Dialogue System/Dialogue Container")]
public class DialogueContainer : ScriptableObject
{
    public List<string> dialogues; // List of dialogues for the NPC

    public string GetDialogue(int index)
    {
        if (index >= 0 && index < dialogues.Count)
        {
            return dialogues[index];
        }
        else
        {
            return "No dialogue available.";
        }
    }
}
