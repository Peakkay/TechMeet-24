using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 
/// Dialogue System UNITY
/// 
/// DONE:
/// - Read dialogue from file
/// - Characters Database
/// 
/// TODO:
/// - Display on UX
/// - On click next line shows
/// - Choice Database
///     - ID
///     - true/false
/// </summary>

[System.Serializable]
public struct SingleDialogue
{
    public string line;
    public Character character;
}
public class Dialogue
{

    SingleDialogue[] lines;
    public int currentLine = 0;
    public int maxLines = 0;
    bool ended = false;

    public Dialogue(string dialogueFilePath)
    {
        Debug.Log("Dialogue: Accesing to '" + Application.persistentDataPath + dialogueFilePath + "'");
        TextAsset textFile = Resources.Load<TextAsset>(dialogueFilePath);
        string dialogueFile = textFile.text;
        JSONNode data = JSON.Parse(dialogueFile);
        JSONArray dialoguesJSON = data.AsArray;
        lines = new SingleDialogue[dialoguesJSON.Count];
        for (int i = 0; i < dialoguesJSON.Count; i++)
        {
            JSONNode line = dialoguesJSON[i];
            lines[i].line = line["dialogue"].Value; //TODO: Add a Character Database and implement it
            lines[i].character = CharacterDatabase.GetSingleton().FindCharacter(line["character"].Value);
        }
        maxLines = lines.Length;
    }

    public bool NextLine() // Returns True if more dialogue can be displayed
    {
        if (currentLine >= maxLines) // Check if there are no more lines
        {
            ended = true;
            DialogueUXManager.instance.HideBox(); // Hide the dialogue box
            return false; // No more dialogue to display
        }

        // Display the current dialogue line
        DialogueUXManager.instance.ShowBox();
        DialogueUXManager.instance.UpdateDialogue(
            lines[currentLine].character.name,
            lines[currentLine].line,
            lines[currentLine].character.color,
            lines[currentLine].character.characterSprite
        );

        currentLine++; // Increment the current line AFTER updating the UI

        if (currentLine >= maxLines) // Check if this was the last line
        {
            ended = true;
        }

        return !ended; // Return whether there are more lines to display
    }

    public void Start()
    {
        /*
        for (int i = 0; i < lines.Length; i++)
        {
            Debug.Log(lines[i].character.name +": " +lines[i].line);
        }*/
    }
}
