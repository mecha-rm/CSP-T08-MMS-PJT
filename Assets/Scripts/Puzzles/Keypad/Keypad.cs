﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the class for the keypad object.
public class Keypad : MonoBehaviour
{
    // the puzzle that this keypad belongs to.
    public Puzzle puzzle;

    // the text for the keypad
    public string text = "*****";

    // the length limit for the text.
    // if it's negative, then there is no limit.
    public int lengthLimit = -1;

    // the passcode for this keypad.
    // there is only one keypad, and this is its passcode, so this is the default.
    public string passcode = "44789";

    // the text display
    public Text textDisplay;

    // Start is called before the first frame update
    void Start()
    {
    }

    // adds text to the text object.
    public void AddCharacter(char ch)
    {
        text += ch.ToString();
    }

    // adds a whole string.
    public void AddString(string str)
    {
        text += str;
    }

    // removes a character.
    public void RemoveLastCharacter()
    {
        // if there are character's to remove.
        if (text.Length > 0)
            text = text.Substring(0, text.Length - 1);
        else // no character's left.
            text = "";
    }

    // clears out the text.
    public void ClearText()
    {
        text = "";
    }

    // used to confirm a text entry.
    public bool ConfirmEntry()
    {
        // checks if text is equal to right passcode.
        bool correct = (text == passcode);

        // output message.
        Debug.Log((correct) ? "SUCCESS" : "FAIL");

        // if the combination is correct and the puzzle is set.
        if (correct && puzzle != null)
            puzzle.OnPuzzleCompletion();

        return correct;
    }

    // Update is called once per frame
    void Update()
    {
        // limits the text length.
        if (lengthLimit >= 0 && text.Length > lengthLimit)
            text = text.Substring(0, lengthLimit);

        // updates the text display every frame.
        if (textDisplay != null)
        {
            // if the text has changed, update it.
            if (textDisplay.text != text)
                textDisplay.text = text;
        }
            
    }
}