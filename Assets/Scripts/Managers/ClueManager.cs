using System;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : Singleton<ClueManager>
{
    public List<Clue> allClues; // List of all clues in the game.
    public List<Clue> discoveredClues = new List<Clue>(); // Clues the player has found.
    public KeyCode jumpLineKey = KeyCode.Space; // Key to close the dialogue box.
    public event Action<Clue> OnClueDiscovered;
    private bool isDialogueBoxOpen = false; // Tracks if the dialogue box is currently open.

    public void DiscoverClue(Clue clue)
    {
        if (!discoveredClues.Contains(clue))
        {
            clue.isDiscovered = true;
            discoveredClues.Add(clue);
            Debug.Log($"Discovered Clue: {clue.clueName}");

            // Show the dialogue box
            DialogueUXManager.instance.ShowBox();
            DialogueUXManager.instance.UpdateDialogue(clue.clueName, clue.description, "#ffffff", clue.clueImage);
            isDialogueBoxOpen = true;
            OnClueDiscovered?.Invoke(clue);
            GraphManager.Instance.UpdateAllNodes();
            // Mark dialogue box as open


            if (clue.PuzzleId != -1)
            {
                PuzzleManager.Instance.CheckPuzzleCompletion(clue.PuzzleId);
            }
        }
    }

    private void Update()
    {
        // Check if the dialogue box is open and the player presses the key to close it
        if (isDialogueBoxOpen && Input.GetKeyDown(jumpLineKey))
        {
            CloseDialogueBox();
        }
    }

    public void CloseDialogueBox()
    {
        // Close the dialogue box and reset the flag
        DialogueUXManager.instance.HideBox();
        isDialogueBoxOpen = false;
    }

    public void DisplayClues()
    {
        foreach (Clue clue in discoveredClues)
        {
            Debug.Log($"Clue: {clue.clueName} - {clue.description}");
        }
    }
}
