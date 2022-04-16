using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// the combination lock (will have 4 numbers)
public class CombinationLock : PuzzleMechanic
{
    // the combination lock entries (four total, but the value can vary).
    [Tooltip("The amount of dials in the lock.")]
    public List<int> entries = new List<int>();

    // the passcode for the combination lock.
    public string combinaton = "1264";

    // list of lock entry objects, which are used for enabling and disabling the mechanic.
    [Tooltip("List of lock entries, which are enabled/disabled along with this component.")]
    public List<LockEntry> lockEntries = new List<LockEntry>(); 

    // displays for the text. This should match up wth the entry list size.
    // NOTE: make it so that you don't need the text displays since the puzzle won't use them.
    public List<Text> textDisplays = new List<Text>();

    // the highest number represented by the lock.
    [Tooltip("The highest number in the lock for [0, highestNumber]. The total amount of numbers is highestNumber + 1 since 0 is included.")]
    public int highestNumber = 9;

    [Header("Audio")]
    // the audio manager for the lock. This is used by the individual keys, but it's put here for ease of access.
    public AudioManager audioManager;

    // the dial turn audio clip.
    public AudioClip dialTurnClip;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // NOTE: text displays are not always needed.
        // no text entries saved (may come in at the wrong order).
        // if(textDisplays.Count == 0)
        // {
        //     GetComponentsInChildren<Text>(textDisplays);
        // 
        // }

        // list not filled.
        if (lockEntries.Count == 0)
        {
            GetComponentsInChildren<LockEntry>(true, lockEntries);
        }          


        // uses the main audio manager.
        if (audioManager == null)
            audioManager = GameplayManager.Current.audioManager;

        // loads up the dial clip.
        if (dialTurnClip == null)
            dialTurnClip = Resources.Load<AudioClip>("Audio/SFXs/SFX_LOCK_CLICKING_CUT");

    }

    // gets the total amount of numbers.
    public int GetNumberCount()
    {
        return highestNumber + 1;
    }


    // sets the entry.
    public void SetEntry(int index, int value)
    {
        // index is valid.
        if (index >= 0 && index < entries.Count)
        {
            // gets the total amount of numbers.
            int numCount = GetNumberCount();

            // adjusts the entry.
            // sets new value.
            // if it's negative, loop around to the start and apply it to numCount.
            entries[index] = (numCount > 0) ? value : numCount + value % numCount;

            // inserts the value into the list.
            // the value range is [0, 9].
            if(value >= numCount) // overbound (loop back to start)
            {
                entries[index] = value % numCount;
            }
            else if(value < 0) // underbound (loop to end)
            {
                // this can result in a value of numCount if value is a multiple of -(numCount).
                // this is because the modulus operation would return 0 in such a case.
                // a later operation takes care of this.
                entries[index] = numCount + value % numCount;
            }
            else // within bounds
            {
                entries[index] = value;
            }

            // positive only
            entries[index] = Mathf.Abs(entries[index]);

            // make sure its in the range of numCount (e.g., [0, 9]).
            // this is needed for loop around cases the value is one more than it should be.
            entries[index] = entries[index] % numCount; 

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

        // debug message. Commented if testing.
        // Debug.Log((correct) ? "CORRECT COMBINATION" : "WRONG COMBINATION");

        // if the combination is correct and the puzzle is set.
        if (correct && puzzle != null)
            puzzle.OnPuzzleCompletion(this);

        // return result
        solved = correct;
        return correct;
    }

    // plays the dial turn sound.
    public void PlayDialTurnSound()
    {
        // plays the sound.
        if (audioManager != null && dialTurnClip != null)
            audioManager.PlayAudio(dialTurnClip);
    }

    // initiates the main action for this puzzle.
    public override void InitiateMainAction()
    {
        // can't interact, so return.
        if (!interactable)
            return;

        // tries out the combination.
        ConfirmCombination();
    }

    // called when the puzzle mechanic component is enabled.
    public override void OnComponentEnable()
    {
        foreach (LockEntry le in lockEntries)
            le.enabled = true;
    }

    // called when the puzzle mechanic component is disabled.
    public override void OnComponentDisable()
    {
        foreach (LockEntry le in lockEntries)
            le.enabled = false;
    }

    // checks if the puzzle was completed successfully successful.
    public override bool IsPuzzleComplete()
    {
        // checks if complete.
        return solved;
    }

    // resets the puzzle.
    public override void ResetMechanic()
    {
        // the mechanic has been reset.
        base.ResetMechanic();

        // resets all entries to 0.
        for (int i = 0; i < entries.Count; i++)
            entries[i] = 0;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
