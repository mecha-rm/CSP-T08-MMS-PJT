using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// puzzle for the lightbox in room 1.
public class Room1LightboxPuzzle : Puzzle
{
    // the gameplay manager.
    public GameplayManager manager;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // if the manager is not set, find it.
        if(manager == null)
            manager = FindObjectOfType<GameplayManager>();
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // TODO: put puzzle completion implementation here.

        //turn on the lights and enable the keypad puzzle
        manager.SetRoomLightingEnabled(true);

        // TODO: turn off puzzle mechanic interactions.
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
