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

    // Start is called before the first frame update
    void Start()
    {
        // tries to find the combination lock.
        if (comboLock == null)
            comboLock = GetComponentInParent<CombinationLock>();
    }

    // on the entry being clocked on.
    private void OnMouseDown()
    {
        // NOTE: OnMouseDown events only work for left mouse clicks, not right, so the second set of code is never reached.
        // As such, it's done elsewhere.

        // goes forward or backwards based on the input.
        if (Input.GetKeyDown(KeyCode.Mouse0)) // left mouse click.
        {
            IncreaseEntryByOne();
        }
    }

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

    // Update is called once per frame
    void Update()
    {
        // if over the button.
        if(overButton)
        {
            // TODO: going in reverse doesn't work because of how you calculate things.
            // going to leave this alone for now, and come back later.

            // OnMouseDown handles the left click, so that's not done here.
            // this is just for the right click.

            // // check for a right-click.
            // if (Input.GetKeyDown(KeyCode.Mouse1)) // right mouse click.
            // {
            //     DecreaseEntryByOne();
            // }
        }
    }
}
