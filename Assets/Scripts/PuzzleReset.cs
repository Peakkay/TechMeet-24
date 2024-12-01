using UnityEngine;

public class PuzzleReset : MonoBehaviour
{
    public GameObject[] wires;

    public void ResetPuzzle()
    {
        foreach (GameObject wire in wires)
        {
            wire.GetComponent<WireInteractable>().ResetPosition();
        }

        WirePuzzleManager.Instance.ResetOrder();
    }
}

