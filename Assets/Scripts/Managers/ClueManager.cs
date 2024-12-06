using System;
using System.Collections.Generic;
using UnityEngine;

public class ClueManager : Singleton<ClueManager>
{
    public List<Clue> allClues; // List of all clues in the game
    public List<Clue> discoveredClues = new List<Clue>(); // Clues the player has found
    public event Action<Clue> OnClueDiscovered; // Event that triggers when a clue is discovered

    private bool isDialogueBoxOpen = false; // Tracks if the clue panel is open
    public KeyCode jumpLineKey = KeyCode.Space; // Key to close the clue panel

    // Method for discovering clues (full process: shows the dialogue box)
    public void DiscoverClue(Clue clue)
    {
        // Ensure the clue has not already been discovered
        if (!discoveredClues.Contains(clue))
        {
            clue.isDiscovered = true; // Mark the clue as discovered
            discoveredClues.Add(clue); // Add the clue to the discovered clues list

            // Log the clue discovery in the console
            Debug.Log($"Discovered Clue: {clue.clueName} - {clue.description}");

            // Show the clue panel with the clue name and description
            DialogueUXManager.instance.ShowBox();
            DialogueUXManager.instance.UpdateDialogue(
                clue.clueName,
                clue.description,
                "#ffffff", // Clue text color
                clue.clueImage
            );

            // Mark the clue panel as open
            isDialogueBoxOpen = true;

            // Trigger the event for clue discovery (useful for other game systems, like puzzles)
            OnClueDiscovered?.Invoke(clue);
            GraphManager.Instance.UpdateAllNodes();

            // If the clue is tied to a puzzle, notify the PuzzleManager (optional)
            if (clue.PuzzleId != -1)
            {
                PuzzleManager.Instance.CheckPuzzleCompletion(clue.PuzzleId);
            }
        }
    }

    // Method for discovering clues tied to NPCs without showing the dialogue box
    public void DiscoverClueFromNPC(Clue clue)
    {
        // Ensure the clue has not already been discovered
        if (!discoveredClues.Contains(clue))
        {
            clue.isDiscovered = true; // Mark the clue as discovered
            discoveredClues.Add(clue); // Add the clue to the discovered clues list

            // Log the clue discovery in the console
            Debug.Log($"Discovered NPC Clue: {clue.clueName} - {clue.description}");

            // Trigger the event for clue discovery (useful for other game systems, like puzzles)
            OnClueDiscovered?.Invoke(clue);
            GraphManager.Instance.UpdateAllNodes();

            // If the clue is tied to a puzzle, notify the PuzzleManager (optional)
            if (clue.PuzzleId != -1)
            {
                PuzzleManager.Instance.CheckPuzzleCompletion(clue.PuzzleId);
            }
        }
    }

    // Method for displaying the discovered clues (this can be used for debugging)
    public void DisplayClues()
    {
        foreach (Clue clue in discoveredClues)
        {
            Debug.Log($"Clue: {clue.clueName} - {clue.description}");
        }
    }

    // Update method to close the clue panel if it is open and the player presses the close key
    private void Update()
    {
        if (isDialogueBoxOpen && Input.GetKeyDown(jumpLineKey))
        {
            CloseDialogueBox();
        }
    }

    // Method to close the dialogue box (if open)
    public void CloseDialogueBox()
    {
        DialogueUXManager.instance.HideBox();
        isDialogueBoxOpen = false;
    }

    // Method to check if the dialogue box is open
    public bool IsDialogueBoxOpen()
    {
        return isDialogueBoxOpen;
    }
}
