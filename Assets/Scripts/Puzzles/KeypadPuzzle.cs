using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for the keypad puzzle.
public class KeypadPuzzle : Puzzle
{
    // the entry into the next room, which is enabled when the puzzle is completed.
    public RoomScreen nextRoomEntry;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // lock the exit door.
        if (nextRoomEntry != null)
            nextRoomEntry.locked = true;
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // unlock the door.
        if (nextRoomEntry != null)
            nextRoomEntry.locked = false;

        // TODO: put puzzle completion implementation here.

        //unlock door in room 1,
        //maybe door object has a state boolean locked/unlocked
    }

    // called when the puzzle is being reset.
    public override void OnPuzzleReset()
    {
        base.OnPuzzleReset();

        // lock the door.
        if (nextRoomEntry != null)
            nextRoomEntry.locked = true;
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
