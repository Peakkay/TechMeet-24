using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string name;
    public string color;
    public string prefix;

    public Sprite characterSprite;

    public Character()
    {
        name = "NONE";
        color = "#ffffff";
        prefix = "";
        characterSprite = null;
    }
}
