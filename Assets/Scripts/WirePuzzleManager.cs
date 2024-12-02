using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleManager : MonoBehaviour
{
    public static WirePuzzleManager Instance;

    private List<int> playerOrder = new List<int>();
    private int totalWires = 7; // The correct number of wires
    private HashSet<WireDragHandler> correctWires = new HashSet<WireDragHandler>();
    public GameObject activePuzzleUI;
    public Puzzle puzzle;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddToOrder(int slotNumber)
    {
        playerOrder.Add(slotNumber);
        CheckOrder();
    }

    public void RemoveFromOrder(int slotNumber)
    {
        playerOrder.Remove(slotNumber);
    }

    private void CheckOrder()
    {
        if (playerOrder.Count != totalWires) return;

        for (int i = 0; i < playerOrder.Count; i++)
        {
            if (playerOrder[i] != i + 1)
            {
                Debug.Log("Order incorrect. Try again!");
                return;
            }
        }

        PuzzleSolved();
    }

    private void PuzzleSolved()
    {
        Debug.Log("Puzzle Solved!");

        if (activePuzzleUI)
        {
            activePuzzleUI.SetActive(false);
            activePuzzleUI = null;
        }
        PuzzleManager.Instance.CompletePuzzle(puzzle);

        // Perform any additional actions (e.g., unlock doors, trigger events)
    }

    public void ResetOrder()
    {
        Debug.Log("Resetting puzzle order...");
        playerOrder.Clear();
    }
    public void WirePlacedCorrectly(WireDragHandler wire)
    {
        if (!correctWires.Contains(wire))
        {
            correctWires.Add(wire);
        }

        // Check if all wires are correctly placed
        if (correctWires.Count == GetTotalWireCount())
        {
            Debug.Log("Puzzle Solved!");
            // Add logic to handle puzzle completion
        }
    }

    private int GetTotalWireCount()
    {
        return FindObjectsOfType<WireDragHandler>().Length;
    }
}

