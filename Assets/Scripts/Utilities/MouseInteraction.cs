using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// a script for tracking mouse interactions with an object in the game world.
// check the booleans to see if the mouse has acted with any of them.
// use this script for tracking object interactions for puzzles.
public class MouseInteraction : MonoBehaviour
{
    // variables
    // you may need to conisder resetting the values after a time.
    private bool mouseDown; // down
    private bool mouseDrag; // drag
    private bool mouseEnter; // enter
    private bool mouseExit; // exit
    private bool mouseOver; // over
    private bool mouseUp; // up
    private bool mouseUpAsButton; // up as button

    // this variable determines how long a variable's value is held before it is reset.
    // if set to 0, it will be reset to false on the next frame.
    // this only applies to certain variables.
    public float holdTime = 0.25F;

    // value used for hold times set to zero.
    // this is done so that the hold time doesn't get reduced every frame.
    private float defaultHoldTime = 0.0001F;

    // mouse up hold time.
    private float mouseUpHoldTime = 0.0F;

    // mouse up as button hold time.
    private float mouseUpAsButtonHoldTime = 0.0F;

    // mouse button pressed over collider.
    private void OnMouseDown()
    {
        mouseDown = true;
        mouseUp = false;
        mouseUpAsButton = false;
    }

    // mouse button pressed over collider and is still being held down.
    private void OnMouseDrag()
    {
        mouseDrag = true;
    }

    // mouse has entered the collider.
    private void OnMouseEnter()
    {
        mouseEnter = true;
        mouseExit = false;
    }

    // mouse has exited the collider.
    private void OnMouseExit()
    {
        mouseExit = true;
        mouseEnter = false;
    }

    // mouse is over the collider.
    private void OnMouseOver()
    {
        mouseOver = true;
        mouseEnter = true;
        mouseExit = false;
    }

    // mouse button released.
    private void OnMouseUp()
    {
        mouseUp = true;
        mouseDown = false;

        // sets this to the hold time.
        mouseUpHoldTime = (holdTime > 0.0F ? holdTime : defaultHoldTime);
    }

    // mouse button released over object it pressed down on.
    private void OnMouseUpAsButton()
    {
        mouseUpAsButton = true;
        mouseDown = false;

        // sets this to the hold time.
        mouseUpAsButtonHoldTime = (holdTime > 0.0F ? holdTime : defaultHoldTime);
    }

    // read-only variables.
    // returns mouse down value.
    public bool MouseDown
    {
        get { return mouseDown; }
    }

    // returns mouse drag value.
    public bool MouseDrag
    {
        get { return mouseDrag; }
    }

    // returns mouse enter value.
    public bool MouseEnter
    {
        get { return mouseEnter; }
    }

    // returns mouse exit value.
    public bool MouseExit
    {
        get { return mouseExit; }
    }

    // reutrns mouse over value.
    public bool MouseOver
    {
        get { return mouseOver; }
    }

    // reutrns mouse up value.
    public bool MouseUp
    {
        get { return mouseUp; }
    }

    // reutrns mouse up as button value.
    public bool MouseUpAsButton
    {
        get { return mouseUpAsButton; }
    }
    
    // resets the mouse variables.
    public void ResetVariables()
    {
        mouseDown = false;
        mouseDrag = false;
        mouseEnter = false;
        mouseExit = false;
        mouseOver = false;
        mouseUp = false;
        mouseUpAsButton = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // if the mouse up hold time is greater than 0.
        if(mouseUpHoldTime > 0.0F)
        {        
            // reduce time.
            mouseUpHoldTime -= Time.deltaTime;

            // hold time is now zero.
            if (mouseUpHoldTime <= 0.0F)
            {
                mouseUp = false;
                mouseUpHoldTime = 0.0F;
            }
                
        }

        // if the mouse up as button hold time is greater than 0.
        if (mouseUpAsButtonHoldTime > 0.0F)
        {
            // reduce time.
            mouseUpAsButtonHoldTime -= Time.deltaTime;

            // hold time is now zero.
            if (mouseUpAsButtonHoldTime <= 0.0F)
            {
                mouseUpAsButton = false;
                mouseUpAsButtonHoldTime = 0.0F;
            }
                
        }
    }
}
