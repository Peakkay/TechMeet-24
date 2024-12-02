using UnityEngine;

public class PuzzleObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Puzzle puzzle; // Assign the Puzzle asset
    [SerializeField] private GameObject wirePuzzleUI; // Assign the wire puzzle UI GameObject

    public void Interact()
    {
        if (!puzzle.isCompleted)
        {
            ShowPuzzleUI();
        }
        else
        {
            Debug.Log($"{puzzle.PuzzleName} is already completed!");
        }
    }

    private void ShowPuzzleUI()
    {
        if (wirePuzzleUI)
        {
            wirePuzzleUI.SetActive(true); // Activate the puzzle UI
        }
        else
        {
            Debug.LogError("WirePuzzleUI is not assigned!");
        }
    }
}
