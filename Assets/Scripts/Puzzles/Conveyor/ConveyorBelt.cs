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
            IsCorrectInputOrder();
        }
            
    }

    // checks if the combination is correct.
    public bool IsCorrectInputOrder()
    {
        // if the amount of inputs does not match, it's automatically false.
        if (buttonInputs.Count != buttonOrder.Count)
            return false;

        // the result of the check.
        bool result = true;

        // the result to check for a reverse order success.
        bool resultReversed = true;


        // gets the button inputs.
        List<ConveyorButton.conveyorButton> order = buttonOrder;

        // goes through each button from start to end.
        for(int i = 0; i < order.Count; i++)
        {
            // reverse index.
            int ir = order.Count - 1 - i;

            // checks forward order.
            if (buttonInputs[i] != order[i])
            {
                result = false;
            }


            // checks reverse order.
            if (buttonInputs[ir] != order[i])
            {
                resultReversed = false;
            }


            // if neither result is true, then the order did not match.
            if (!result && !resultReversed)
                break;
        }

        // wrong button combination, so clear out the inputs.
        if (!result && !resultReversed)
            buttonInputs.Clear();


        // saves the content.
        complete = result;
        completeReversed = resultReversed;

        // the combination is in the right order.
        if (result)
            Debug.Log("Combination in right order.");
        else
            Debug.Log("Failed in right order.");

        // the combination is in the reverse order.
        if (resultReversed)
            Debug.Log("Combination in reverse order.");
        else
            Debug.Log("Failed in reverse order.");


        // clears out the button inputs.
        buttonInputs.Clear();

        // the puzzle is complete.
        if (result && puzzle != null)
            puzzle.OnPuzzleCompletion();

        // returns the result.
        return result;
    }

    // checks if the input order is in reverse.
    public bool IsReverseInputOrder()
    {
        IsCorrectInputOrder();

        // this value has been changed, so return it.
        return completeReversed;
    }

    // initiates the main action for this puzzle.
    public override void InitiateMainAction()
    {
        // can't interact with this puzzle, so don't allow this.
        if (!interactable)
            return;

        IsCorrectInputOrder();
    }

    // only successful if completed in the right order.
    public override bool IsPuzzleComplete()
    {
        return complete;
    }

    // resets the puzzle.
    public override void ResetPuzzle()
    {
        complete = false;
        buttonInputs.Clear(); // clears out inputs.

        // called to reset the puzzle.
        if (puzzle != null)
            puzzle.OnPuzzleReset();

        // TODO: restore conveyor belt to default.

    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
