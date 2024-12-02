using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPuzzle", menuName = "PuzzleSystem/Puzzle")]
public class Puzzle : ScriptableObject
{
    public string PuzzleName;
    public string description;
    public Sprite clueImage; // Optional: for visual clues.
    public bool isCompleted = false;
    public List<Clue> requiredClues;
}
