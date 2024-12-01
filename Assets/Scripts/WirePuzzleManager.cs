using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleManager : MonoBehaviour
{
    public static WirePuzzleManager Instance;

    private List<int> playerOrder = new List<int>();
    private int totalWires = 7; // The correct number of wires

    [SerializeField] private GameObject successEffect; // Effect to display when puzzle is solved

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
        if (successEffect) Instantiate(successEffect, transform.position, Quaternion.identity);

        // Perform additional actions (e.g., unlock door, trigger next puzzle)
    }
}

