using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPuzzle : Puzzle
{
    // TODO: seperate the elevator bell from the elevator door.
    // the elevator for this puzzle.
    public Elevator elevator;

    // the gate for the elevator.
    public GameObject gate;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // tries to find the elevator.
        if (elevator == null)
            GetComponentInChildren<Elevator>();
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // TODO: put puzzle completion implementation here.

        // TODO: do an animation instead of just hiding it.
        if (gate != null)
            gate.SetActive(false);
    }

    // shows the gate again.
    public override void OnPuzzleReset()
    {
        base.OnPuzzleReset();

        if (gate != null)
            gate.SetActive(true);
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
