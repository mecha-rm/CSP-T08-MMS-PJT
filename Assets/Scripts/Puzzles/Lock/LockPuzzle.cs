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
    public Vector3 baseLeftDoorEulers;

    // auto set the left door reset.
    public bool autoSetLeftDoorReset = true;

    // the right door for the closet.
    public GameObject rightDoor;

    // the base rightward rotation.
    public Vector3 baseRightDoorEulers;

    // auto set the right door reset.
    public bool autoSetRightDoorReset = true;

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
            comboLock = GetComponentInChildren<CombinationLock>(true);

        // if the combo lock is set.
        if(comboLock != null)
        {
            // not in list, so add it.
            if (!mechanics.Contains(comboLock))
                mechanics.Add(comboLock);
        }

        // saves the rotation for the left door.
        if (leftDoor != null && autoSetLeftDoorReset)
            baseLeftDoorEulers = leftDoor.transform.eulerAngles;

        // saves the rotation for the right door.
        if (rightDoor != null && autoSetLeftDoorReset)
            baseRightDoorEulers = rightDoor.transform.eulerAngles;
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // hides lock
        if(comboLock != null)
            comboLock.gameObject.SetActive(false);
        
        // TODO: add animation to lock opening

        // the puzzle screen is no longer accessible.
        if(puzzleScreen != null)
        {
            // makes the player go back a screen.
            manager.SwitchToBackScreen();

            // turns off this screen.
            puzzleScreen.locked = true; // lock the screen.
            puzzleScreen.gameObject.SetActive(false);
        }

        // open the doors.
        // open left if it isn't already open.
        if (leftDoor != null && leftDoor.transform.eulerAngles == baseLeftDoorEulers)
            leftDoor.transform.Rotate(leftDoor.transform.up, -theta);

        // open right if it isn't already open.
        if (rightDoor != null && rightDoor.transform.eulerAngles == baseRightDoorEulers)
            rightDoor.transform.Rotate(leftDoor.transform.up, theta);

    }

    // reset the puzzle.
    public override void OnPuzzleReset()
    {
        base.OnPuzzleReset();

        // reset happens in the base function.
        if (comboLock != null)
        {
            comboLock.gameObject.SetActive(true);
        }

        // the puzzle screen is now accessible.
        if (puzzleScreen != null)
        {
            puzzleScreen.locked = false; // unlock the screen.
            puzzleScreen.gameObject.SetActive(true);
        }

        // close the doors.
        // close left
        if (leftDoor != null)
            leftDoor.transform.eulerAngles = baseLeftDoorEulers;

        // close right
        if (rightDoor != null)
            rightDoor.transform.eulerAngles = baseRightDoorEulers;

    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
