using UnityEngine;
using TMPro;

public class LightPatternPuzzle : MonoBehaviour
{
    public TMP_Text hintText; // Optional TMP_Text for displaying hints or feedback
    public GameObject[] keypadButtons; // Buttons on the keypad
    private int[] correctSequence = { 0, 2, 1, 3 }; // Example sequence
    private int currentStep = 0;
    public bool solved;
    void Start()
    {
        foreach (var button in keypadButtons)
        {
            button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnButtonPressed(button));
        }
    }

    void OnButtonPressed(GameObject pressedButton)
    {
        int index = System.Array.IndexOf(keypadButtons, pressedButton);

        if (index == correctSequence[currentStep])
        {
            currentStep++;
            hintText.text = $"Step {currentStep}/{correctSequence.Length} completed!";
            if (currentStep >= correctSequence.Length)
            {
                Debug.Log("Light pattern puzzle solved!");
                MultiPuzzleManager.Instance.CompletePuzzle("LightPattern");
                gameObject.SetActive(false); // Deactivate the panel
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<PlayerMovement>().canMove = true;
                solved = true;
            }
        }
        else
        {
            currentStep = 0;
            hintText.text = "Incorrect sequence. Try again!";
        }
    }
}
