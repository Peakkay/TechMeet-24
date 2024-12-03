using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EncryptedTerminal : MonoBehaviour
{
    public TMP_Text displayText; // UI Text to show the encrypted word and key
    public TMP_InputField inputField; // Input field for user guesses
    public Button submitButton; // Button to submit the guess
    public TMP_Text feedbackBox;
    private string[] words = { "UNITY", "CIPHER", "PUZZLE", "ESCAPE", "MATRIX" }; // Pool of words to encrypt
    private string encryptedText; // The encrypted text
    private string originalWord; // The original word
    private int encryptionKey; // The random key

    private void Start()
    {
        GenerateNewPuzzle(); // Generate a new encrypted word when the game starts
        submitButton.onClick.AddListener(CheckAnswer); // Hook up the submit button
    }

    private void GenerateNewPuzzle()
    {
        // Pick a random word
        originalWord = words[Random.Range(0, words.Length)];
        Debug.Log(originalWord);
        // Generate a random key between 1 and 25
        encryptionKey = Random.Range(1, 26);

        // Encrypt the word using the key
        encryptedText = EncryptWithCaesarCipher(originalWord, encryptionKey);

        // Display the encrypted text and key
        displayText.text = $"{encryptedText}\nKey: {encryptionKey}";
    }

    private string EncryptWithCaesarCipher(string text, int key)
    {
        char[] buffer = text.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];

            // Shift the letter and wrap it around if necessary
            letter = (char)(letter + key);
            if (letter > 'Z')
            {
                letter = (char)(letter - 26);
            }
            buffer[i] = letter;
        }
        return new string(buffer);
    }

    private void CheckAnswer()
    {
        string userInput = inputField.text.ToUpper();

        if (userInput == originalWord)
        {
            feedbackBox.text = "Access Granted";
            feedbackBox.color = Color.green; // Set text color to green
            Debug.Log("Correct Answer! Puzzle Solved.");
            StartCoroutine(LoadNextSceneWithDelay(2f));
        }
        else
        {
            feedbackBox.text = "Access Denied";
            feedbackBox.color = Color.red;
            Debug.Log("Incorrect Answer. Try Again.");

        }

        // Clear input field after submission
        inputField.text = "";
    }
    private IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        SceneManager.LoadScene("NextSceneName"); // change it to room2 scene
    }
}
