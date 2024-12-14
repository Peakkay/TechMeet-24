using UnityEngine;

public class Interactable : MonoBehaviour,IInteractable
{
    public string puzzleName; // Assign Math, LightPattern, or HiddenKey in Inspector
    public  MathPuzzle mp;
    public LightPatternPuzzle lp;

    public void Interact()
    {
        if(puzzleName == "Math" && mp.solved)
        {
            return;
        }
        if(puzzleName == "LightPattern" && lp.solved)
        {
            return;
        }
        MultiPuzzleManager.Instance.ActivatePuzzle(puzzleName);
    }
}
