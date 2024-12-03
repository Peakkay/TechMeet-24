using UnityEngine;
using TMPro;

public class PasswordInputUI : MonoBehaviour
{
    // UI elements for the three digit displays
    [SerializeField]
    private TMP_Text[] digitDisplays;

    // Reference to the LockedDrawer script
    [SerializeField]
    private LockedDrawer lockedDrawer;

    // Array to store the current values of the digits
    private int[] digits = new int[3];

    private void Start()
    {
        // Initialize all digits to 0
        UpdateDigitDisplays();
    }

    // Increment a specific digit
    public void IncrementDigit(int index)
    {
        Debug.Log("Pressed");
        if (index < 0 || index >= digits.Length) return;

        digits[index] = (digits[index] + 1) % 10; // Wrap around to 0 after 9
        UpdateDigitDisplays();
    }

    // Decrement a specific digit
    public void DecrementDigit(int index)
    {
        if (index < 0 || index >= digits.Length) return;

        digits[index] = (digits[index] - 1 + 10) % 10; // Wrap around to 9 before 0
        UpdateDigitDisplays();
    }

    // Update the digit display texts
    private void UpdateDigitDisplays()
    {
        for (int i = 0; i < digitDisplays.Length; i++)
        {
            digitDisplays[i].text = digits[i].ToString();
        }
    }

    // Submit the entered password
    public void SubmitPassword()
    {
        string enteredPassword = string.Join("", digits); // Combine digits into a string
        Debug.Log("Entered Password: " + enteredPassword);

        if (lockedDrawer != null)
        {
            lockedDrawer.Interact(enteredPassword);
        }
        else
        {
            Debug.LogWarning("LockedDrawer reference is missing!");
        }
    }
}
