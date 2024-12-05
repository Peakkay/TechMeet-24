using UnityEngine;

public class MultiPuzzleManager : MonoBehaviour
{
    public static MultiPuzzleManager Instance;
    public GameObject MathPuzzlePanel;
    public GameObject LightPatternPanel;
    public GameObject HiddenKeyPanel;
    public bool mathComplete = false;
    public bool lightComplete = false;
    public bool keyComplete = false;
    public GameObject activePuzzleUI;
    public Puzzle puzzle;

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
        Debug.Log(puzzleName + "completed");
        switch (puzzleName)
        {
            case "Math":
                mathComplete = true;
                break;
            case "LightPattern":
                lightComplete = true;
                break;
            case "HiddenKey":
                keyComplete = true;
                break;
        }
        TryCompleteAll();
    }

    public void TryCompleteAll()
    {
        if(mathComplete && keyComplete && lightComplete)
        {
            Debug.Log("AllCompleted");
            if (activePuzzleUI)
            {
                activePuzzleUI.SetActive(false);
                activePuzzleUI = null;
            }
            DeactivateAllPuzzles();
            PuzzleManager.Instance.CompletePuzzle(puzzle);
        }
    }
}
