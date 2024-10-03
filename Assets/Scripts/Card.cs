using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public static string[] colorWords = {"black", "red", "green", "blue", "yellow"};
    public static Color[] colors = {Color.black, Color.red, Color.green, Color.blue, Color.yellow};

    public int wordCount, colorWord, color;

    public void Init(int wordCount, int colorWord, int color) {
        this.wordCount = wordCount;
        this.colorWord = colorWord;
        this.color = color;

        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        text.color = colors[color];
        string finalText = colorWords[colorWord];
        for (int i = 1; i < wordCount; i++) {
            finalText += "\n" + colorWords[colorWord];
        }
        text.text = finalText;
    }

    public static bool IsValidCombination(Card a, Card b) {
        return a.wordCount != b.wordCount && a.colorWord != b.colorWord && a.color != b.color 
            && a.colorWord != b.color && a.color != b.colorWord;
    }
}
