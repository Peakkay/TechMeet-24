using System.Collections.Generic;
using UnityEngine;

public class ClueManager : Singleton<ClueManager>
{
    public List<Clue> allClues; // List of all clues in the game.
    public List<Clue> discoveredClues = new List<Clue>(); // Clues the player has found.

    public void DiscoverClue(Clue clue)
    {
        if (!discoveredClues.Contains(clue))
        {
            clue.isDiscovered = true;
            discoveredClues.Add(clue);
            Debug.Log($"Discovered Clue: {clue.clueName}");
            if(clue.PuzzleId != -1)
            {
                PuzzleManager.Instance.CheckPuzzleCompletion(clue.PuzzleId);
            }
        }
    }

    public void DisplayClues()
    {
        foreach (Clue clue in discoveredClues)
        {
            Debug.Log($"Clue: {clue.clueName} - {clue.description}");
        }
    }
}
