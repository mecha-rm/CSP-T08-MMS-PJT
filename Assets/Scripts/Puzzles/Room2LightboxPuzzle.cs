using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room2LightboxPuzzle : Puzzle
{
    // the gameplay manager.
    public GameplayManager manager;

    // the player of the game.
    public Player player;

    [Header("Linked Objects")]

    // the note for room 2 (only given when the puzzle is solved).
    // TODO: maybe make it so that doing the maze opens the door.
    public NoteItem note;

    // enables the four maze doors.
    public MazeBox mazeBox1;
    public MazeBox mazeBox3;
    public MazeBox mazeBox2;
    public MazeBox mazeBox4;

    // can't play maze until the lights are on.

    // Start is called before the first frame update
    protected new void Start()
    {
        // call the parent's start.
        base.Start();

        // if the manager is not set, find it.
        if (manager == null)
            manager = GameplayManager.Current;

        // grabs the player.
        if (player == null)
            player = Player.Current;

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
        // if(mazeBox1 != null)
        //     mazeBox1.

        // turn on the note.
        if (note != null && !player.HasItem(note))
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
