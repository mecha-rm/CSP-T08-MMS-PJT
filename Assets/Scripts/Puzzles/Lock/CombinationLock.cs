using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the combination lock (will have 4 numbers)
public class CombinationLock : PuzzleMechanic
{
    // the combination lock entries (four total, but the value can vary).
    public List<int> entries = new List<int>();

    // the passcode for the combination lock.
    public string combinaton = "1264";

    // displays for the text. This should match up wth the entry list size.
    // NOTE: make it so that you don't need the text displays since the puzzle won't use them.
    public List<Text> textDisplays = new List<Text>();

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        // no text entries saved (may come in at the wrong order).
        // if(textDisplays.Count == 0)
        // {
        //     GetComponentsInChildren<Text>(textDisplays);
        // 
        // }
    }

    // sets the entry.
    public void SetEntry(int index, int value)
    {
        // index is valid.
        if (index >= 0 && index < entries.Count)
        {
            // adjusts the entry.
            entries[index] = value; // sets new value.
            entries[index] = Mathf.Abs(entries[index]); // positive only
            entries[index] = entries[index] % 9; // range of [0, 9]

            // updates the text display.
            if (index < textDisplays.Count)
            {
                if (textDisplays[index] != null)
                    textDisplays[index].text = entries[index].ToString();
            }
        }

        // entry has changed, so comfirm the combination.
        ConfirmCombination();
    }

    // changes an entry by a given amount. Only [0, 9] are allowed.
    public void AddToEntry(int index, int amount)
    {
        // index is valid.
        if(index >= 0 && index < entries.Count)
        {
            // set the entry.
            SetEntry(index, entries[index] + amount);
        }
    }

    // increases the entry.
    public void IncreaseEntryByOne(int index)
    {
        AddToEntry(index, 1);
    }

    // decreases the entry.
    public void DecreaseEntryByOne(int index)
    {
        AddToEntry(index, -1);
    }

    // checks if the combination has been confirmed.
    public bool ConfirmCombination()
    {
        // holds the numbers as a string.
        string number = "";
        
        // adds all entries to the string.
        foreach(int num in entries)
        {
            number += num.ToString();
        }

        // gets the result.
        bool correct = number == combinaton;

        // debug message.
        // TODO: uncomment.
        Debug.Log((correct) ? "CORRECT COMBINATION" : "WRONG COMBINATION");

        // if the combination is correct and the puzzle is set.
        if (correct && puzzle != null)
            puzzle.OnPuzzleCompletion();

        // return result
        return correct;
    }

    // checks if the puzzle was completed successfully successful.
    public override bool CompleteSuccess()
    {
        // checks if complete.
        return ConfirmCombination();
    }

    // resets the puzzle.
    public override void ResetPuzzle()
    {
        // resets all entries to 0.
        for (int i = 0; i < entries.Count; i++)
            entries[i] = 0;

        // called to reset the puzzle.
        if (puzzle != null)
            puzzle.OnPuzzleReset();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
