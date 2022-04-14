using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a door in the maze.
public class MazeBox : PuzzleMechanic
{
    // open value is off.
    public bool isOpen = false;

    // can only open the door, not close it.
    public bool openOnly = true;

    // the door for the maze box.
    public GameObject door;

    // te base door rotation.
    public Vector3 closedDoorEulers;

    // the rotation factor for opening the door.
    public float theta = 180.0F;

    // the object inside the box.
    public GameObject storedObject;

    // Start is called before the first frame update
    void Start()
    {
        // hide the puzzle peice as if it's stored.
        if (storedObject != null)
            storedObject.SetActive(false);

        // saves the door's orientation.
        if (door != null)
            closedDoorEulers = door.transform.eulerAngles;
    }

    // Called when the user clicks on the box.
    private void OnMouseDown()
    {
        InitiateMainAction();
    }

    // opens the door.
    public void OpenDoor()
    {
        // if the door is already open, do nothing.
        if (isOpen)
            return;

        // opens the door.
        if (door != null && !isOpen)
            door.transform.Rotate(door.transform.up, -theta);

        // shows the object in the box.
        if (storedObject != null)
            storedObject.SetActive(true);

        isOpen = true;
        solved = true;
    }

    // closes the door.
    public void CloseDoor()
    {
        // if the door is already open, do nothing.
        if (!isOpen)
            return;

        // reset to base rotation.
        if (door != null && isOpen)
            door.transform.eulerAngles = closedDoorEulers;

        // hides the object in the box.
        if (storedObject != null)
            storedObject.SetActive(false);

        isOpen = false;
        solved = false;

    }

    // initiates the main action.
    public override void InitiateMainAction()
    {
        // opens the door.
        if (!isOpen)
            OpenDoor();
        // closes the door if the door can be closed.
        else if (isOpen && !openOnly)
            CloseDoor();
    }

    // component is enabled.
    public override void OnComponentEnable()
    {
        // N/A
    }

    // component is disabled.
    public override void OnComponentDisable()
    {
        // N/A
    }

    // checks if the puzzle is complete.
    public override bool IsPuzzleComplete()
    {
        return solved;
    }

    // resets the puzzle.
    public override void ResetPuzzle()
    {
        // closes the door.
        isOpen = true; // makes sure it will close.
        CloseDoor();
    }

    // Update is called once per frame
    protected new void Update()
    {

    }

    
}
