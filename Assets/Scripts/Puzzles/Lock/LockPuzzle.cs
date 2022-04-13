using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// puzzle for the lock.
public class LockPuzzle : Puzzle
{
    // the gameplay manager.
    public GameplayManager manager;

    // the combination lock.
    public CombinationLock comboLock;

    // the screen for the puzzle.
    public RoomScreen puzzleScreen;

    [Header("Doors")]

    // the left door for the closet.
    public GameObject leftDoor;

    // the base leftward rotation.
    public Quaternion baseLeftDoorRot;

    // the right door for the closet.
    public GameObject rightDoor;

    // the base rightward rotation.
    public Quaternion baseRightDoorRot;

    // rotation factor
    [Tooltip("The rotation amount.")]
    public float theta = 180;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // grabs the gameplay manager.
        if (manager == null)
            manager = GameplayManager.Current;

        // grabs the combination lock in the children.
        if (comboLock == null)
            comboLock = GetComponentInChildren<CombinationLock>();

        // saves the rotation for the left door.
        if (leftDoor != null)
            baseLeftDoorRot = leftDoor.transform.rotation;

        // saves the rotation for the right door.
        if (rightDoor != null)
            baseRightDoorRot = rightDoor.transform.rotation;
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // TODO: put puzzle completion implementation here.

        // hides lock
        if(comboLock != null)
            comboLock.gameObject.SetActive(false);
        // TODO: open closed doors and add animation to lock opening

        // the puzzle screen is no longer accessible.
        if(puzzleScreen != null)
        {
            // makes the player go back a screen.
            manager.SwitchToBackScreen();

            // TODO: open the door.

            // turns off this screen.
            puzzleScreen.gameObject.SetActive(false);
        }

        // open the doors.
        // open left
        if (leftDoor != null)
            leftDoor.transform.Rotate(leftDoor.transform.up, -theta);

        // open right
        if (rightDoor != null)
            rightDoor.transform.Rotate(leftDoor.transform.up, theta);

    }

    // reset the puzzle.
    public override void OnPuzzleReset()
    {
        base.OnPuzzleReset();

        // TODO: call reset instead (needs a rework)?
        if (comboLock != null)
        {
            comboLock.gameObject.SetActive(true);

            // no longer solved.
            comboLock.solved = false;

            // resets all entries to 0.
            for (int i = 0; i < comboLock.entries.Count; i++)
                comboLock.entries[i] = 0;
        }

        // close the doors.
        // close left
        if (leftDoor != null)
            leftDoor.transform.rotation = baseLeftDoorRot;

        // close right
        if (rightDoor != null)
            rightDoor.transform.rotation = baseRightDoorRot;

    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
