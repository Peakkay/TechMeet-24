using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class StarMapManager : MonoBehaviour
{
    public static StarMapManager Instance; // Singleton instance

    public GameObject lineDrawerPrefab; // Prefab with LineRenderer attached
    private List<GameObject> selectedStars = new List<GameObject>(); // Stores player-selected stars
    public List<GameObject> correctSequence; // Stores the correct sequence of stars (assign via Inspector)

    public TMP_Text feedbackText; // UI Text to display feedback
    public TMP_Text clueText; // UI Text to display the clue
    public GameObject activePuzzleUI;
    public Puzzle puzzle;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Set the initial clue text
        if (clueText != null)
        {
            Debug.Log("Clue Text is assigned.");
            clueText.text = "Clue: Connections form the answer.";
        }



    }

    public void StarSelected(GameObject star)
    {
        if (!selectedStars.Contains(star))
        {
            selectedStars.Add(star);

            // Connect the stars visually if more than one star is selected
            if (selectedStars.Count > 1)
            {
                GameObject previousStar = selectedStars[selectedStars.Count - 2];
                ConnectStars(previousStar, star);
            }

            // Check if the sequence is correct so far
            if (selectedStars[selectedStars.Count - 1] != correctSequence[selectedStars.Count - 1])
            {
                ShowFeedback("Wrong sequence! Try again.", Color.red);
                ResetPuzzle();
                return;
            }

            // If the sequence is complete, validate the puzzle
            if (selectedStars.Count == correctSequence.Count)
            {
                ShowFeedback("Correct sequence! Puzzle solved!", Color.green);
                OnPuzzleSolved();
            }
        }
    }

    private void ConnectStars(GameObject star1, GameObject star2)
    {
        if (lineDrawerPrefab == null)
        {
            Debug.LogError("LineDrawerPrefab is not assigned in StarMapManager.");
            return;
        }

        if (star1 == null || star2 == null)
        {
            Debug.LogError("One or both stars are null.");
            return;
        }

        GameObject lineObject = Instantiate(lineDrawerPrefab, transform);
        LineDrawer lineDrawer = lineObject.GetComponent<LineDrawer>();

        if (lineDrawer == null)
        {
            Debug.LogError("LineDrawer component is missing on the prefab.");
            return;
        }

        lineDrawer.DrawLine(star1.transform.position, star2.transform.position);
    }

    private void ResetPuzzle()
    {
        // Clear selected stars
        selectedStars.Clear();

        // Destroy all line connections
        foreach (Transform child in transform)
        {
            if (child.GetComponent<LineRenderer>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        // Clear feedback text
        if (feedbackText != null)
        {
            Debug.Log("Feedback Text is assigned.");
            feedbackText.text = "";
        }
    }

    private void ShowFeedback(string message, Color color)
    {
        if (feedbackText != null)
        {
            Debug.Log("Feedback: " + message);
            feedbackText.text = message;
            feedbackText.color = color;
        }
    }

    private void OnPuzzleSolved()
    {
        // Actions to perform when the puzzle is solved
        Debug.Log("Congratulations! The puzzle is solved.");
        if (activePuzzleUI)
        {
            activePuzzleUI.SetActive(false);
            activePuzzleUI = null;
        }
        PuzzleManager.Instance.CompletePuzzle(puzzle);

    }
}
