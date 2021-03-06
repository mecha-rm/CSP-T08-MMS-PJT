using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// an entry into the lock.
public class LockEntry : MonoBehaviour
{
    // the combination lock.
    public CombinationLock comboLock;

    // the index of the combination lock.
    public int index = 0;

    // becomes 'true' when the mouse is over the collider.
    // this is used for the right mouse click.
    private bool overButton = false;

    [Header("Action")]

    // the dial for the lock.
    [Tooltip("The dial for this lock entry. By default, it's the object this script is attached to.")]
    public GameObject dial;

    // Start is called before the first frame update
    void Start()
    {
        // tries to find the combination lock.
        if (comboLock == null)
            comboLock = GetComponentInParent<CombinationLock>();

        // this is the dial.
        if (dial == null)
            dial = gameObject;
    }

    // NOTE: for consistency, the left and right click have been put in the same place.
    // // on the entry being clicked on.
    // private void OnMouseDown()
    // {
    //     // NOTE: OnMouseDown events only work for left mouse clicks, not right, so the second set of code is never reached.
    //     // As such, it's done elsewhere.
    // 
    //     // goes forward or backwards based on the input.
    //     if (Input.GetKeyDown(KeyCode.Mouse0)) // left mouse click.
    //     {
    //         IncreaseEntryByOne();
    //     }
    // }

    // called when the mouse is over a collider element.
    private void OnMouseOver()
    {
        overButton = true;   
    }

    // called when the mouse exits the collider.
    private void OnMouseExit()
    {
        overButton = false;
    }

    // increases the lock entry by 1.
    public void IncreaseEntryByOne()
    {
        if (comboLock != null)
            comboLock.IncreaseEntryByOne(index);
    }

    // decreases the lock entry by 1.
    public void DecreaseEntryByOne()
    {
        if (comboLock != null)
            comboLock.DecreaseEntryByOne(index);
    }

    // rotates the dial by 36 degrees every click (10 sided shape, 360/10 = 36)
    public void RotateDial(bool positive)
    {
        // rotates the dial.
        if(dial != null)
        {
            // the angle of rotation.
            float theta = 360.0F / comboLock.GetNumberCount();

            // rotates the dial.
            if(positive)
                dial.transform.Rotate(0, 0, theta);
            else
                dial.transform.Rotate(0, 0, -theta);

            // plays the dial turn audio.
            comboLock.PlayDialTurnSound();
        }
    }


    // Update is called once per frame
    void Update()
    {
        // if the combo lock is set.
        if(comboLock != null)
        {
            // can't interact with the puzzle, so don't do anything.
            if (!comboLock.interactable)
                return;
        }

        // if over the button.
        if (overButton)
        {
            // going to leave this alone for now, and come back later.

            // OnMouseDown handles the left click, so that's not done here.
            // this is just for the right click.

            // check for a right-click.
            if (Input.GetKeyDown(KeyCode.Mouse0)) // left mouse click.
            {
                IncreaseEntryByOne();
                RotateDial(true);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1)) // right mouse click.
            {
                DecreaseEntryByOne();
                RotateDial(false);
            }
        }
    }
}
