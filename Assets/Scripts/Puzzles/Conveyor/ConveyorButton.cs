using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a button for the conveyor belt.
public class ConveyorButton : MonoBehaviour
{
    // enum for the conveyor buttons.
    public enum conveyorButton { pwave, swave, surface }

    // the conveyor belt this button is attached to.
    public ConveyorBelt conveyor;

    // the key number for this conveyor button.
    public conveyorButton button;

    // TODO: add space for animation.

    // Start is called before the first frame update
    void Start()
    {
        // tries to find the conveyor belt.
        if (conveyor == null)
            conveyor = GetComponentInParent<ConveyorBelt>();
    }

    // called when the mouse button is clicked down.
    private void OnMouseDown()
    {
        PerformAction();
    }

    public void PerformAction()
    {
        // perform action on left mouse click.
        if (Input.GetKeyDown(KeyCode.Mouse0))
            AddInput();
    }


    // adds an input to the conveyor.
    public void AddInput()
    {
        // adds an input if this is set.
        if (conveyor != null)
        {
            // can't interact with the puzzle, so don't do anything.
            if (!conveyor.interactable)
                return;

            // adds the input.
            conveyor.AddInput(button);

            // plays the button sound.
            conveyor.PlayButtonSound();
        }
            
    }
}
