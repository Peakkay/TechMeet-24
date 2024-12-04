using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogEntry : MonoBehaviour
{
    // UI elements for displaying log data
    public TMP_Text TimestampText;
    public TMP_Text IPAddressText;
    public TMP_Text ActionText;

    private LogEntryData logData; // Holds the log entry data
    private IntrusionDetectionPuzzle manager; // Reference to the puzzle manager

    // Method to initialize the log entry with data
    public void Initialize(LogEntryData data, IntrusionDetectionPuzzle puzzleManager)
    {
        logData = data;
        manager = puzzleManager;

        // Populate the text fields with the log data
        TimestampText.text = data.Timestamp;
        IPAddressText.text = data.IPAddress;
        ActionText.text = data.Action;
    }

    // Called when the log entry is clicked
    public void OnClick()
    {
        manager.OnLogEntryClicked(logData, gameObject);
    }
}
