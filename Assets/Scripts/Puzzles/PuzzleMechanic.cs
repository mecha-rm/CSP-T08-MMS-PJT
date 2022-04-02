using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a script for the puzzle mechanic.
public abstract class PuzzleMechanic : MonoBehaviour
{
    // the puzzle that this keypad belongs to.
    public Puzzle puzzle;

    // TODO: have puzzle enabled

    // TODO: make puzzle start?

    // checks if the puzzle is complete in every update.
    public bool checkCompleteInUpdate = true;

    // Start is called before the first frame update
    protected void Start()
    {
        // ...
    }

    // checks if the puzzle is complete.
    public abstract bool CompleteSuccess();

    // resets the puzzle.
    // this makes sure that PuzzleMechanic is an abstract class.
    // this doesn't need any actual code in it.
    public abstract void ResetPuzzle();

    // Update is called once per frame
    protected virtual void Update()
    {
        // if the puzzle's completion should be checked in every update.
        if(checkCompleteInUpdate)
        {
            // checks if the puzzle is available.
            if(puzzle != null)
            {
                // check if the puzzle was successful.
                bool complete = CompleteSuccess();

                // if complete, call for puzzle completion.
                if (complete)
                    puzzle.OnPuzzleCompletion();
            }
            
        }
    }
}
