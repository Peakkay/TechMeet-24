using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LogEntryData
{
    public string Timestamp;  // E.g., "2024-12-04 14:23:10"
    public string IPAddress;  // E.g., "192.168.1.10"
    public string Action;     // E.g., "Unauthorized access"
    public bool IsSuspicious; // True if the entry is suspicious
}

public class IntrusionDetectionPuzzle : MonoBehaviour
{
    public List<LogEntryData> logEntries; // List of all log entries
    public GameObject logEntryPrefab; // Prefab for log entry UI elements
    public Transform logContent; // The Content object inside the Scroll View
    public Text feedbackText; // UI Text for displaying feedback

    private int suspiciousCount; // Total number of suspicious entries
    private int foundCount; // Number of suspicious entries the player has found
    public GameObject activePuzzleUI;
    public Puzzle puzzle;
    public PuzzleObject starter;

    void Start()
    {
        // Initialize log entries (example data)
        logEntries = new List<LogEntryData>
        {
            new LogEntryData { Timestamp = "2024-12-04 14:20:10", IPAddress = "192.168.1.10", Action = "Login attempt", IsSuspicious = false },
            new LogEntryData { Timestamp = "2024-12-04 14:21:00", IPAddress = "203.0.113.5", Action = "Unauthorized access", IsSuspicious = true },
            new LogEntryData { Timestamp = "2024-12-04 14:22:30", IPAddress = "192.168.1.15", Action = "File download", IsSuspicious = false },
            new LogEntryData { Timestamp = "2024-12-04 14:23:15", IPAddress = "203.0.113.5", Action = "External device connected", IsSuspicious = true },
            new LogEntryData { Timestamp = "2024-12-04 14:53:45", IPAddress = "123.0.108.9", Action = "Cloud accessed", IsSuspicious = true },
            new LogEntryData { Timestamp = "2024-12-04 14:53:45", IPAddress = "123.0.108.9", Action = "File download", IsSuspicious = true }
        };

        // Count the number of suspicious entries
        suspiciousCount = logEntries.FindAll(entry => entry.IsSuspicious).Count;

        // Populate the UI with log entries
        PopulateLogUI();
    }

    // Populates the scroll view with log entries
    void PopulateLogUI()
    {
        foreach (LogEntryData entry in logEntries)
        {
            // Instantiate a new log entry UI element
            GameObject logItem = Instantiate(logEntryPrefab, logContent);

            // Get the LogEntry script on the prefab and initialize it
            LogEntry logScript = logItem.GetComponent<LogEntry>();
            logScript.Initialize(entry, this);
        }
    }

    // Handles player interaction when a log entry is clicked
    public void OnLogEntryClicked(LogEntryData entry, GameObject logItem)
    {
        if (entry.IsSuspicious)
        {
            // Player found a suspicious entry
            foundCount++;
            feedbackText.text = "Correct! Suspicious entry found.";
            feedbackText.color = Color.green;
            // Highlight in green
        }
        else
        {
            // Player clicked on a legitimate entry
            feedbackText.text = "Incorrect! This is a legitimate entry.";
            feedbackText.color = Color.red;
        }

        // Check if all suspicious entries are found
        if (foundCount == suspiciousCount)
        {
            feedbackText.text = "You found all suspicious entries! Puzzle solved.";
            feedbackText.color = Color.green;
            UnlockNextStage();
        }
    }

    // Unlocks the next stage of the game or puzzle
    void UnlockNextStage()
    {
        Debug.Log("Puzzle completed. Proceed to the next stage!");
        if (activePuzzleUI)
        {
            activePuzzleUI.SetActive(false);
            activePuzzleUI = null;
        }
        PuzzleManager.Instance.CompletePuzzle(puzzle);
        starter.puzzleOpen = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerMovement>().canMove = true;
    }
}
