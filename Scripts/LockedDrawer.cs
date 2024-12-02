using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDrawer : MonoBehaviour
{
    private string correctPasscode = "123"; // Password (testing purpose 123)
    
    public GameObject keycard; //key

    public PlayerInventory player; //Player ki inventory
    private bool isUnlocked = false; 

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
            player.AddItem(keycard);
            Debug.Log("Key Given");  //Replcae this with adding item in inventory method
        }
        else
        {
            Debug.LogWarning("Keycard reference is missing!");
        }
    }
}
