using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewClue", menuName = "Clue System/Clue")]
public class Clue : ScriptableObject
{
    public string clueName;
    public string description;
    public Sprite clueImage; // Optional: for visual clues.
    public bool isDiscovered = false;
}



