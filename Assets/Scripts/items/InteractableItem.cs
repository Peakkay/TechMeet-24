using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public Item item; // The item that can be picked up
    public Clue clue; // The clue associated with this object.

    public void Interact()
    {
        InventoryManager.Instance.AddItem(item);
        if (clue != null)
        {
            ClueManager.Instance.DiscoverClue(clue);
            Debug.Log($"Interacted with {clue.clueName}");
        }
        Destroy(gameObject); // Remove the item from the world after pickup
    }
}

