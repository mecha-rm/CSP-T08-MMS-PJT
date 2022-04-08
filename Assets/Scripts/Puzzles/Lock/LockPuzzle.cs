using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPuzzle : Puzzle
{
    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // TODO: put puzzle completion implementation here.

        // unlock latch clear from screen, tirgger closet to open
        
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
