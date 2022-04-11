using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// conveyor puzzle
public class ConveyorBeltPuzzle : Puzzle
{
    // the conveyor belt.
    public ConveyorBelt conveyor;

    [Tooltip("The reward object, which contains the treasure and the puzzle piece.")]
    public GameObject reward;

    // the certificate object.
    [Tooltip("A certificate that's given upon putting the inputs in reverse order.")]
    public Item certificate;

    // called when there has been a reverse win.
    public bool reverseWin = false;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // tries to find the conveyor belt.
        if (conveyor == null)
            conveyor = GetComponentInChildren<ConveyorBelt>();
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // activates the reward object.
        if (reward != null)
            reward.SetActive(true);
    }

    // called when a puzzle fails.
    public override void OnPuzzleFailure()
    {
        // called when the puzzle fails.
        base.OnPuzzleFailure();

        // clear out the button inputs.
        if (conveyor != null)
            conveyor.buttonInputs.Clear();
    }

    // called when the puzzle is being reset.
    public override void OnPuzzleReset()
    {
        base.OnPuzzleReset();

        // the conveyor belt does this already, but this is to make sure these values have been changed.
        if(conveyor != null)
        {
            conveyor.buttonInputs.Clear();
            conveyor.complete = false;
            conveyor.completeReversed = false;
        }

        // deactivate the reward.
        if (reward != null)
            reward.SetActive(false);

        // certificate is set.
        if (certificate != null)
        {
            // deactivate object.
            certificate.gameObject.SetActive(false);
            
            // remove the certificate from the player's inventory.
            Player.Current.TakeItem(certificate.itemId);
        }

        // no reverse win.
        reverseWin = false;


    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();

        // if the reversed conveyor is complete, show the certificate.
        if(conveyor.completeReversed && !reverseWin)
        {
            // TODO: run an animation instead of enable the certificate.
            
            // activate the certificate.
            if(certificate != null)
                certificate.gameObject.SetActive(true);

            // stops it from constantly enabling the certificate.
            reverseWin = true;

        }
    }
}
