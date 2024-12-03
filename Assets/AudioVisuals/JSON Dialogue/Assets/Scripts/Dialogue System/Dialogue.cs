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

    public bool NextLine() //Retruns True if more dialogue can be displayed
    {
        DialogueUXManager.instance.ShowBox();
        if (currentLine < maxLines)
        {
            DialogueUXManager.instance.UpdateDialogue(lines[currentLine].character.name, lines[currentLine].line, lines[currentLine].character.color);
        }
        //Debug.Log(lines[currentLine].character.name + ": " + lines[currentLine].line);
        currentLine++;
        if (currentLine > maxLines)
        {
            ended = true;
            DialogueUXManager.instance.HideBox();
        }
        return !ended;
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
