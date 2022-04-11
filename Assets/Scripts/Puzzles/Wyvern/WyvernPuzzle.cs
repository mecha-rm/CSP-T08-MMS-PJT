using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WyvernPuzzle : Puzzle
{
    // the wyvern for the puzzle.
    public Wyvern wyvern;

    // the delay for the wyvern disappearing.
    // this is needed because the player ends up clicking the wall and erasing the text otherwise.
    // this may not be needed when using an animation though.
    [Tooltip("The countdown timer for the wyvern to disappear.")]
    public float wyvernDelay = 0.0F;

    // delay time max.
    [Tooltip("The delay time it takes for the wyvern to disappear.")]
    public float wyvernDelayMax = 0.02F;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // tries to find the wyvern.
        if (wyvern == null)
            wyvern = GetComponentInChildren<Wyvern>(true);
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // // turn off the wyvern.
        // if(wyvern != null)
        // {
        //     // gives it the wyvern's descriptor.
        //     GameplayManager.Current.SetDescriptor(wyvern.desc);
        // }

        // set to max time.
        wyvernDelay = wyvernDelayMax;
    }

    // called when the puzzle is being reset.
    public override void OnPuzzleReset()
    {
        // called to reset the puzzle.
        base.OnPuzzleReset();

        // removes treasure.
        if(wyvern != null)
        {
            wyvern.hasTreasure = false;
        }

        wyvernDelay = 0.0F;
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();

        // disappear timer
        if (wyvern != null && finished && wyvernDelay > 0)
        {
            // countdown
            wyvernDelay -= Time.deltaTime;

            // make wyvern disappear.
            if (wyvernDelay <= 0)
            {
                wyvern.gameObject.SetActive(false);
                wyvernDelay = 0.0F;
            }
        }
    }
}
