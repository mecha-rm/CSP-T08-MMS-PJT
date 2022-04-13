using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2LightboxPuzzle : Puzzle
{
    // the gameplay manager.
    public GameplayManager manager;

    // the note for room 2 (only given when the puzzle is solved).
    public NoteItem note;

    // can't play maze until the lights are on.

    // Start is called before the first frame update
    protected new void Start()
    {
        // call the parent's start.
        base.Start();

        // if the manager is not set, find it.
        if (manager == null)
            manager = GameplayManager.Current;

        // note is not set.
        if (note == null)
            note = GetComponentInChildren<NoteItem>();

        // hide the note.
        if (note != null)
            note.gameObject.SetActive(false);
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        //turn on the lights and enable the keypad puzzle
        manager.SetRoomLightingEnabled(true);

        // TODO: turn off puzzle mechanic interactions.

        // turn on the note.
        if (note != null)
            note.gameObject.SetActive(true);
    }

    // called when the puzzle is being reset.
    public override void OnPuzzleReset()
    {
        base.OnPuzzleReset();

        // turns off the lights.
        manager.SetRoomLightingEnabled(false);

        // hide the note.
        if (note != null)
            note.gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call the parent's update.
        base.Update();
    }
}
