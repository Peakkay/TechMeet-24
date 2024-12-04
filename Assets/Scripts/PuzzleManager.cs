using UnityEngine;

public class MultiPuzzleManager : MonoBehaviour
{
    public static MultiPuzzleManager Instance;
    public GameObject MathPuzzlePanel;
    public GameObject LightPatternPanel;
    public GameObject HiddenKeyPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Activate a specific puzzle
    public void ActivatePuzzle(string puzzleName)
    {
        DeactivateAllPuzzles();

        switch (puzzleName)
        {
            case "Math":
                MathPuzzlePanel.SetActive(true);
                break;
            case "LightPattern":
                LightPatternPanel.SetActive(true);
                break;
            case "HiddenKey":
                HiddenKeyPanel.SetActive(true);
                break;
            default:
                Debug.LogError($"Puzzle {puzzleName} not found!");
                break;
        }
    }

    // Deactivate all puzzles
    private void DeactivateAllPuzzles()
    {
        MathPuzzlePanel.SetActive(false);
        LightPatternPanel.SetActive(false);
        HiddenKeyPanel.SetActive(false);
    }

    public void CompletePuzzle(string puzzleName)
    {
        Debug.Log($"Puzzle {puzzleName} completed!");
        // Add logic for vault unlocking or progression if needed
    }
}
