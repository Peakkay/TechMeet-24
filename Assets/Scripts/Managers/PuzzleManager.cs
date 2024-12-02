using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    public List<Puzzle> allPuzzles; // List of all clues in the game.

    public void CompletePuzzle(Puzzle p)
    {
        p.isCompleted = true;
        Debug.Log(p.name + "is Completed");
    }
    public void CheckPuzzleCompletion(int id)
    {
        foreach(Puzzle p in allPuzzles)
        {
            if(id == p.PuzzleId)
            {
                int flag = 1;
                foreach(Clue c in p.requiredClues)
                {
                    if(!c.isDiscovered)
                    {
                        flag = 0;
                    }
                }
                if(flag == 1)
                {
                    Debug.Log("All Clues for" + p.PuzzleName + "have been Discovered");
                    CompletePuzzle(p);
                }
            }
        }
    }
}
