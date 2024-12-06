using UnityEngine;
using UnityEngine.UI;

public class FinalVerdictManager : MonoBehaviour
{
    public Button[] suspectButtons; // Array for suspect buttons
    public Button confirmButton;
    private int selectedSuspectIndex = -1;

    void Start()
    {
        // Add listeners to suspect buttons
        for (int i = 0; i < suspectButtons.Length; i++)
        {
            int index = i; // Capture index for closure
            suspectButtons[i].onClick.AddListener(() => SelectSuspect(index));
        }

        // Add listener to confirm button
        confirmButton.onClick.AddListener(ConfirmChoice);
    }

    void SelectSuspect(int index)
    {
        // Update the selected suspect index
        selectedSuspectIndex = index;

        // Reset all button colors to default (e.g., white)
        foreach (Button button in suspectButtons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;  // Default color
            colors.selectedColor = Color.white; // Default selected color
            button.colors = colors;
        }

        // Change the selected button's color to green
        ColorBlock selectedColors = suspectButtons[index].colors;
        selectedColors.normalColor = Color.green;  // Highlight color
        selectedColors.selectedColor = Color.green; // Selected state
        suspectButtons[index].colors = selectedColors;

        Debug.Log($"Suspect {index + 1} selected and highlighted.");
    }

    void HighlightSelected(int index)
    {
        // Reset button colors
        foreach (Button button in suspectButtons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = Color.white;
            button.colors = colors;
        }

        // Highlight the selected button
        ColorBlock selectedColors = suspectButtons[index].colors;
        selectedColors.normalColor = Color.red;
        suspectButtons[index].colors = selectedColors;
    }

   void ConfirmChoice()
    {
        // Check if a suspect has been selected
        if (selectedSuspectIndex == -1)
        {
            Debug.Log("No suspect selected! Please select a suspect before confirming.");
            return;
        }

        // Lock other suspect buttons
        for (int i = 0; i < suspectButtons.Length; i++)
        {
            if (i == selectedSuspectIndex)
            {
                // Keep the confirmed suspect interactable
                suspectButtons[i].interactable = true;
                
                // Change color to red for confirmation
                ColorBlock confirmedColors = suspectButtons[i].colors;
                confirmedColors.normalColor = Color.red;  // Confirmed color
                confirmedColors.selectedColor = Color.red; // Selected color
                suspectButtons[i].colors = confirmedColors;
            }
            else
            {
                // Disable other buttons
                suspectButtons[i].interactable = false;
            }
        }

        Debug.Log($"Final verdict confirmed: Suspect {selectedSuspectIndex + 1}");
        EndingManager.Instance.ChooseEnding(selectedSuspectIndex);
    }


}
