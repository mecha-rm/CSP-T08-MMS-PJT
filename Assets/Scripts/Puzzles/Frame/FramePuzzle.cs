using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used for the frame puzzle.
public class FramePuzzle : Puzzle
{
    // the frame object.
    public Frame frame;

    // the elevator's game object.
    public GameObject elevatorGroup;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // Tries to find the frame.
        if (frame == null)
            frame = GetComponentInChildren<Frame>(true);
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // TODO: fade out of existence.

        // activates the elevator group.
        if (elevatorGroup != null)
            elevatorGroup.SetActive(true);

        // TODO: put puzzle completion implementation here.
    }

    // called when the puzzle is being reset.
    public override void OnPuzzleReset()
    {
        // called to reset the puzzle.
        base.OnPuzzleReset();

        // deactivates the elevator group.
        if (elevatorGroup != null)
            elevatorGroup.SetActive(false);
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
