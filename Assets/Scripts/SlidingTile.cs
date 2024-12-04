using UnityEngine;

public class SlidingTile : MonoBehaviour
{
    public Vector2Int gridPosition; // Position in the grid
    public SlidingPuzzleManager puzzleManager; // Reference to Puzzle Manager

    public void OnInteract()
    {
        Debug.Log("Tried");
        puzzleManager?.TryMoveTile(this);
    }
}
