using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MathPuzzle : MonoBehaviour
{
    [Header("Puzzle Components")]
    [Tooltip("Enter the numbers directly here.")]
    public List<string> numberValues; // Strings representing the numbers, editable from the Inspector
    public TMP_InputField sumInput; // TMP_InputField for entering the sum
    public GameObject submitButton; // Button to submit the answer
    public PuzzleObject starter;

    private int correctSum;

    void Start()
    {
        correctSum = 0;

        // Convert the entered number values to integers and calculate the total sum
        foreach (var value in numberValues)
        {
            if (int.TryParse(value, out int number))
            {
                correctSum += number;
            }
            else
            {
                Debug.LogWarning($"Invalid number format in list: {value}");
            }
        }

        // Add listener for the submit button
        submitButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CheckAnswer);
    }

    void CheckAnswer()
    {
        if (int.TryParse(sumInput.text, out int userSum) && userSum == correctSum)
        {
            Debug.Log("Math puzzle solved!");
            MultiPuzzleManager.Instance.CompletePuzzle("Math");
            starter.puzzleOpen = false;
            gameObject.SetActive(false); // Deactivate the panel
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerMovement>().canMove = true;
        }
        else
        {
            Debug.Log("Incorrect sum. Try again!");
        }
    }
}
