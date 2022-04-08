using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a script for the puzzle mechanic.
public abstract class PuzzleMechanic : MonoBehaviour
{
    // the puzzle that this keypad belongs to.
    public Puzzle puzzle;

    // becomes 'true' when the puzzle is solved.
    [Tooltip("Becomes 'true' when the puzzle is completed.")]
    public bool solved;

    // TODO: have puzzle enabled

    // TODO: make puzzle start?

    // the puzzle mechanic can be interacted with.
    public bool interactable = true;

    // checks if the puzzle is complete in every update.
    public bool checkCompleteInUpdate = true;

    // Start is called before the first frame update
    protected void Start()
    {
        // ...
    }

    // iniates the main action from the puzzle.
    public abstract void InitiateMainAction();

    // checks if the puzzle is complete.
    public abstract bool IsPuzzleComplete();

    // resets the puzzle.
    // this makes sure that PuzzleMechanic is an abstract class.
    // this doesn't need any actual code in it.
    public abstract void ResetPuzzle();

    // Update is called once per frame
    protected virtual void Update()
    {
        // if you can't interact with the puzzle mechanic, don't do anything.
        if (!interactable)
            return;

        // if the puzzle's completion should be checked in every update.
        if(checkCompleteInUpdate)
        {
            // checks if the puzzle is available.
            if(puzzle != null)
            {
                // check if the puzzle was successful.
                bool complete = IsPuzzleComplete();

                // if complete, call for puzzle completion.
                if (complete)
                    puzzle.OnPuzzleCompletion();
            }
            
        }
    }
}
