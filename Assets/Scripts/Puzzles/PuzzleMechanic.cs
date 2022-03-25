using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a script for the puzzle mechanic.
public abstract class PuzzleMechanic : MonoBehaviour
{
    // the puzzle that this keypad belongs to.
    public Puzzle puzzle;

    // TODO: make puzzle start?

    // checks if the puzzle is complete.
    public abstract bool CompleteSuccess();

    // resets the puzzle.
    // this makes sure that PuzzleMechanic is an abstract class.
    // this doesn't need any actual code in it.
    public abstract void ResetPuzzle();
}
