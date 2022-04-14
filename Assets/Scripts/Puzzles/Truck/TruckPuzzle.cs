﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the truck puzzle.
public class TruckPuzzle : Puzzle
{
    // the truck.
    public Truck truck;

    // the box collider for the ligthbox screen.
    public RoomScreen lightboxScreen;

    // the trigger for the maze screen.
    public ScreenTrigger mazeScreenTrigger;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // tries to find the truck.
        if (truck == null)
            GetComponentInChildren<Truck>();

        // locks the lightbox screen.
        if (lightboxScreen != null)
            lightboxScreen.locked = true;

        // hide the screen trigger.
        if (mazeScreenTrigger != null)
            mazeScreenTrigger.gameObject.SetActive(false);
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // locks the lightbox screen.
        if (lightboxScreen != null)
            lightboxScreen.locked = false;

        // shows the screen trigger.
        if (mazeScreenTrigger != null)
            mazeScreenTrigger.gameObject.SetActive(true);
    }

    // shows the gate again.
    public override void OnPuzzleReset()
    {
        base.OnPuzzleReset();

        // need to rework the reset function.
        // if (truck != null)
        //     truck.ResetPuzzle();

        // locks the lightbox screen.
        if (lightboxScreen != null)
            lightboxScreen.locked = true;

        // hide the screen trigger.
        if (mazeScreenTrigger != null)
            mazeScreenTrigger.gameObject.SetActive(false);
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
