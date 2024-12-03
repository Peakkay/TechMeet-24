using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDatabase
{
    private Character[] characters;

    private CharacterDatabase()
    {
        Debug.Log("CharacterDatabase: Accesing to '" + Application.persistentDataPath + "Characters/Database'");
        TextAsset textFile = Resources.Load<TextAsset>("Characters/Database");
        string dialogueFile = textFile.text;
        JSONNode data = JSON.Parse(dialogueFile);
        JSONArray charactersJSON = data.AsArray;
        characters = new Character[charactersJSON.Count];
        for (int i = 0; i < data.Count; i++)
        {
            JSONNode jsonCharacter = charactersJSON[i];
            characters[i] = new Character();
            characters[i].name = jsonCharacter["name"].Value;
            characters[i].color = jsonCharacter["color"].Value;
            characters[i].prefix = jsonCharacter["base-Image"].Value;
            Debug.Log("CharacterDatabase: " + characters[i].name + " added to database");
        }
    }
    private static readonly CharacterDatabase _singleton = new CharacterDatabase();

    public static CharacterDatabase GetSingleton()
    {
        return _singleton;
    }
    public Character FindCharacter(string name)
    {
        //Find in this.characters if name exists and return object

        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i].name == name)
            {
                return characters[i];
            }
        }
        return new Character();
    }
}
