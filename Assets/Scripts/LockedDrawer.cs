using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDrawer : MonoBehaviour
{
    private string correctPasscode = "123"; // Password (testing purpose 123)
    
    public Item keycard; //key

    private bool isUnlocked = false; 
    public GameObject activePuzzleUI;
    public Puzzle puzzle;
    public PuzzleObject starter;

    public void Interact(string inputCode)
    {
        if (isUnlocked)
        {
            Debug.Log("The drawer is already unlocked!"); // Aesthjetic somethinhg for already unlocked
            return;
        }

        if (inputCode == correctPasscode)
        {
            UnlockDrawer();
            Debug.Log("Puzzle Solved!");

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
        else
        {
            Debug.Log("Incorrect passcode. Try again."); //Aesthetic something for wrong password
        }
    }

    private void UnlockDrawer()
    {
        isUnlocked = true;
        Debug.Log("Drawer unlocked! You have received a keycard."); // Khul gaya type kuch
        
        if (keycard != null)
        {
            InventoryManager.Instance.AddItem(keycard);
            Debug.Log("Key Given");  //Replcae this with adding item in inventory method
        }
        else
        {
            Debug.LogWarning("Keycard reference is missing!");
        }
    }
}
