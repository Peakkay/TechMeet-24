using UnityEngine;

public class PuzzleObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Puzzle puzzle; // Assign the Puzzle asset
    [SerializeField] private GameObject PuzzleUI;
    [SerializeField] private string puzzleStarterId; // Unique ID for debugging
    public bool puzzleOpen = false;

    public void Update()
    {
        if(puzzleOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log($"Escaping PuzzleUI for: {puzzle.PuzzleName} ({puzzle.PuzzleId})");
            PuzzleUI.SetActive(false); // Activate the puzzle UI
            puzzleOpen = false;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerMovement>().canMove = true;
        }
    }
    public void Interact()
    {
        Debug.Log($"Interacting with: {gameObject.name} {puzzleStarterId}");
        Debug.Log($"Assigned Puzzle: {puzzle.PuzzleName} (ID: {puzzle.PuzzleId})");
        
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
        if (PuzzleUI)
        {
            Debug.Log($"Activating PuzzleUI for: {puzzle.PuzzleName} ({puzzle.PuzzleId})");
            PuzzleUI.SetActive(true); // Activate the puzzle UI
            puzzleOpen = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerMovement>().canMove = false;
        }
        else
        {
            Debug.LogError("WirePuzzleUI is not assigned!");
        }
    }
}
