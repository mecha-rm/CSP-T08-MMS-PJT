using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is for a puzzle.
// NOTE: inherit this script when putting in unique puzzle behaviour. This is meant to be specialized to a given puzzle.
public class Puzzle : MonoBehaviour
{
    // becomes 'true' if the puzzle is finished.
    // NOTE: if a puzzle has multiple parts, only call OnPuzzleCompletion when all of them are done.
    public bool finished = false;

    // the description of the puzzle.
    public Descriptor desc;

    // becomes 'true' when the puzzle is complete.
    // commented out because the puzzle might have multiple parts.
    // public bool puzzleComplete = false;

    // TODO: make list for puzzle interactions.

    // Start is called before the first frame update
    protected void Start()
    {
        // finds the component attached to this object.
        if (desc == null)
            desc = GetComponent<Descriptor>();
    }

    // TODO: make on puzzle failure.

    // called when the puzzle is completed.
    public virtual void OnPuzzleCompletion()
    {
        // override this function for the individual puzzle.
        
        // TODO: comment out this message.
        if (!finished)
            Debug.Log("Puzzle Complete!");

        finished = true;

    }

    // called when a puzzle fails.
    public virtual void OnPuzzleFailure()
    {
        finished = false;
    }

    // called when the puzzle is being reset.
    public virtual void OnPuzzleReset()
    {
        finished = false;
    }

    // Update is called once per frame
    protected void Update()
    {
       // ... 
    }
}
