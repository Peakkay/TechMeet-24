using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindKey : MonoBehaviour, IInteractable
{
    public int keyID;
    public void Interact()
    {
        if(InventoryManager.Instance.HasItem(keyID,1))
        {
            MultiPuzzleManager.Instance.CompletePuzzle("HiddenKey");
        }
    }
}
