using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPuzzle", menuName = "PuzzleSystem/Puzzle")]
public class Puzzle : ScriptableObject
{
    public string PuzzleName;
    public string description;
    public int PuzzleId;
    public bool isCompleted = false;
    public List<Clue> requiredClues;
    public GameObject puzzleInterface;
}
