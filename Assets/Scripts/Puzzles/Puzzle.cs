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

    // TODO: rewrite complete function so that multiple mechanics can be saved for one puzzle?
    // the list of puzzle mechanics. These are enabled and disabled as needed.
    [Tooltip("The list of puzzle mechanics, which have their scripts enabled/disabled based on the puzzle.")]
    public List<PuzzleMechanic> mechanics = new List<PuzzleMechanic>();

    // Start is called before the first frame update
    protected void Start()
    {
        // finds the component attached to this object.
        if (desc == null)
            desc = GetComponent<Descriptor>();
    }

    // This function is called when the object becomes enabled or active.
    private void OnEnable()
    {
        EnablePuzzleMechanics();
    }

    // This function is called when the object becomes disabled or inactive.
    private void OnDisable()
    {
        DisablePuzzleMechanics();
    }

    // called to enable the puzzle.
    public virtual void EnablePuzzleMechanics()
    {
        // enables all puzzle mechanics.
        foreach (PuzzleMechanic m in mechanics)
            m.enabled = true;
    }

    // called to disable the puzzle.
    public virtual void DisablePuzzleMechanics()
    {
        // enables all puzzle mechanics.
        foreach (PuzzleMechanic m in mechanics)
            m.enabled = false;
    }

    // called when the puzzle is completed.
    public virtual void OnPuzzleCompletion()
    {
        // override this function for the individual puzzle.
        
        // TODO: comment out this message.
        if (!finished)
            Debug.Log("Puzzle Complete!");

        finished = true;

    }

    // calls the on puzzle completion functon with the mechanic that was just completed.
    public virtual void OnPuzzleCompletion(PuzzleMechanic mechanic)
    {
        // TODO: if you were using multiple mechanics you could check which one finished.

        OnPuzzleCompletion();
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

    // resets hte puzzle.
    public void ResetPuzzle()
    {
        // calls this to run all puzzle reset functions.
        OnPuzzleReset();
    }

    // Update is called once per frame
    protected void Update()
    {
       // ... 
    }
}
