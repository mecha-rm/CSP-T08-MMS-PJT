using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a door in the maze.
// TODO: implement the doors.
public class MazeDoor : MonoBehaviour
{
    // open value is off.
    public bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Called when the user clicks on the box.
    private void OnMouseDown()
    {
        // opens the door.
        if (!isOpen)
            OpenDoor();
    }

    // opens the door.
    public void OpenDoor()
    {
        isOpen = true;
    }

    // closes the door.
    public void CloseDoor()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
