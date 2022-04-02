using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the script for the conveyor belt.
public class ConveyorBelt : PuzzleMechanic
{
    // the combination for the conveyor belt.
    // Order: Surface -> S-Wave -> P-Wave
    public List<ConveyorButton.conveyorButton> buttonOrder = new List<ConveyorButton.conveyorButton>();

    // the button inputs for the conveyor belt.
    // ths gets cleared when the amount of inputs reaches the length of the order.
    public List<ConveyorButton.conveyorButton> buttonInputs = new List<ConveyorButton.conveyorButton>();

    // becomes 'true', when the puzzle is complete.
    public bool complete = false;

    // becomes 'true' if the conveyor belt is completed in reverse.
    public bool completeReversed = false;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // fills in the default combination.
        if(buttonOrder.Count == 0)
        {
            buttonOrder.Add(ConveyorButton.conveyorButton.surface);
            buttonOrder.Add(ConveyorButton.conveyorButton.swave);
            buttonOrder.Add(ConveyorButton.conveyorButton.pwave);
        }
    }


    // adds an input to the conveyor belt.
    public void AddInput(ConveyorButton.conveyorButton button)
    {
        // adds the button.
        buttonInputs.Add(button);

        // checks for the correct input.
        if (buttonInputs.Count == buttonOrder.Count)
        {
            IsCorrectInputOrder(false);
            IsCorrectInputOrder(true);
        }
            
    }

    // checks if the combination is correct.
    // if 'reversed' is true, then it checks if the combination is correct in reverse.
    public bool IsCorrectInputOrder(bool reversed)
    {
        // if the amount of inputs does not match, it's automatically false.
        if (buttonInputs.Count != buttonOrder.Count)
            return false;

        // the result of the check.
        bool result = true;

        // gets the button inputs.
        List<ConveyorButton.conveyorButton> order = buttonOrder;

        // TODO: set this up so that it sets the value to false if it fails again.

        // reverses the order of the list.
        if (reversed)
            order.Reverse();

        // goes through each button..
        for(int i = 0; i < order.Count; i++)
        {
            // if a mismatch is found, then the wrong order was inputed.
            if(buttonInputs[i] != order[i])
            {
                result = false;
                break;
            }
        }

        // wrong button combination, so clear out the inputs.
        if (!result)
            buttonInputs.Clear();

        // checks if things were reversed or not.
        if (reversed)
        {
            completeReversed = result;
        } 
        else
        {
            complete = result;
        }

        // prints a message.
        // TODO: take out print result.
        if (result)
            Debug.Log("COMBINATION" + ((reversed) ? "REVERSE SUCCESS!" : "SUCCESS!"));
        else
            Debug.Log("FAILED.");
            

        // TODO: animate the conveyor belt.

        // returns the result.
        return result;
    }

    // checks if the combination is correct in the right order.
    public bool IsCorrectInputOrder()
    {
        return IsCorrectInputOrder(false);
    }

    // checks if the inputs match the combination in reverse.
    public bool IsCorrectInputOrderReversed()
    {
        return IsCorrectInputOrder(true);
    }

    // only successful if completed in the right order.
    public override bool CompleteSuccess()
    {
        return complete;
    }

    // resets the puzzle.
    public override void ResetPuzzle()
    {
        complete = false;
        buttonInputs.Clear(); // clears out inputs.

        // TODO: restore conveyor belt to default.
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
