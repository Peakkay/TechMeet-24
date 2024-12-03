using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueUXManager : MonoBehaviour
{
    public static DialogueUXManager instance { get; private set; }

    public GameObject uiDialogueBox;
    public Text uiTextName;
    public Text uiTextBody;
    public Image uiCharimage;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void Start()
    {
        uiDialogueBox.SetActive(false);
    }
    public void HideBox()
    {
        uiDialogueBox.SetActive(false);
    }
    public void ShowBox()
    {
        uiDialogueBox.SetActive(true);
    }
    public void UpdateDialogue(string charName, string bodyText, string colorText = "#ffffff", Sprite spriteCharacter = null)
    {
        //UPDATE UX

        uiTextName.text = charName;
        Color dialColor = new Color();
        ColorUtility.TryParseHtmlString(colorText, out dialColor);
        uiTextName.color = dialColor;
        uiTextBody.text = bodyText;


        if (spriteCharacter != null) //If Image exists Show on screen
        {
            
        }
    }
}
