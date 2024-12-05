using UnityEngine;

public class Interactable : MonoBehaviour,IInteractable
{
    public string puzzleName; // Assign Math, LightPattern, or HiddenKey in Inspector

    public void Interact()
    {
        MultiPuzzleManager.Instance.ActivatePuzzle(puzzleName);
    }
}
