using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{

//    public GameObject questcanvas;
//    public GameObject inventorycanvas;


//    bool questUIopen = false;
//    bool invuiopen = false;
    public PlayerMovement playerMovement;
    public Sprite upFacingSprite;      // Assign your Up Facing sprite
    public Sprite downFacingSprite;    // Assign your Down Facing sprite
    public Sprite rightFacingSprite;   // Assign your Right Facing sprite
    private SpriteRenderer spriteRenderer;    

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    //    questcanvas.SetActive(false);
    //    inventorycanvas.SetActive(false);
    }

    private void Update()
    {
        if (playerMovement.canMove) // Check if movement is allowed
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        // Check for movement keys and ensure the player is not currently moving
        if (!playerMovement.IsMoving) // Check if the player is currently moving
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerMovement.Move(Vector3.up);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                playerMovement.Move(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                playerMovement.Move(Vector3.down);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                playerMovement.Move(Vector3.right);
            }
            else if(Input.GetKeyDown(KeyCode.Q))
            {
/*               if(!questUIopen)
                {
                    // SceneManager.LoadScene("QuestsUI", LoadSceneMode.Additive);
                     questUIopen = true;

                    questcanvas.SetActive(true);
                    questcanvas.GetComponent<QuestUIManager>().showQuestUI();
                }
                else
                {
                    // SceneManager.UnloadSceneAsync("QuestsUI");
                     questUIopen  = false;
                    questcanvas.SetActive(false);
                } */
            }

            else if(Input.GetKeyDown(KeyCode.I))
            {
/*
                //SceneManager.LoadScene("inventory");
                if(invuiopen == false)
                {
                inventorycanvas.SetActive(true);
                inventorycanvas.GetComponent<inventoryUIManager>().Start();
                }
*/       
            }
        }
    }
}
