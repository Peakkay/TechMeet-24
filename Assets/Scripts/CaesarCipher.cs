using System.Collections.Generic;
using UnityEngine;

public static class CaesarCipher
{
    private static readonly List<string> WordPool = new List<string>
    {
        "UNITY", "CIPHER", "PUZZLE", "ENCRYPT", "DECRYPT", "GAMING", "PLAYER"
    };

    public static string Encrypt(string input, int key)
    {
        char[] buffer = input.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];

            if (char.IsLetter(letter))
            {
                char offset = char.IsUpper(letter) ? 'A' : 'a';
                letter = (char)((letter + key - offset) % 26 + offset);
            }

            buffer[i] = letter;
        }
        return new string(buffer);
    }

    public static string Decrypt(string input, int key)
    {
        return Encrypt(input, 26 - key);
    }

    public static (string encrypted, string original, int key) GenerateRandomEncryptedWord()
    {
        int key = Random.Range(1, 26); // Random shift key
        string originalWord = WordPool[Random.Range(0, WordPool.Count)]; // Random word
        string encryptedWord = Encrypt(originalWord, key);
        return (encryptedWord, originalWord, key);
    }
}
