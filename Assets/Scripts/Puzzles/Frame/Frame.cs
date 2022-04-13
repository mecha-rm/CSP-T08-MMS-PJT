using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle frame.
public class Frame : PuzzleMechanic
{
    // gameplay manager.
    public GameplayManager manager;

    // the game object for the complete puzzle.
    public GameObject completePuzzleObject;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // saves the manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // hides the complete puzzle model.
        if (completePuzzleObject != null)
            completePuzzleObject.SetActive(solved);

    }

    // called when the mouse is pressed down.
    private void OnMouseDown()
    {
        // perform action on left mouse click.
        if (Input.GetKeyDown(KeyCode.Mouse0))
            PlayerHasAllPuzzlePieces();
    }

    // checks to see if the player has all the pieces.
    public bool PlayerHasAllPuzzlePieces()
    {
        // can't interact with the puzzle, so don't do anything.
        if (!interactable)
        {
            Debug.Log("Puzzle is disabled, so this will always return false.");
            return false;
        }            

        // checks player for puzzle pieces.
        bool result = manager.player.HasAllPuzzlePieces();

        // if this was a success.
        if(result)
        {
            // removes all of the puzzle pieces from the player.
            manager.player.TakeItems(Item.PUZZLE_PIECE_ID);

            // turns on the model.
            if (completePuzzleObject != null)
                completePuzzleObject.SetActive(true);

            // puzzle has been completed.
            if (puzzle != null)
                puzzle.OnPuzzleCompletion(this);

            Debug.Log("All pieces inputed.");
        }
        else
        {
            // turns off the model.
            if (completePuzzleObject != null)
                completePuzzleObject.SetActive(false);

            Debug.Log("Missing pieces still.");
        }

        // TODO: add in puzzle functionality.

        // send back result.
        solved = result;
        return result;
    }

    // initiates the main action for this puzzle.
    public override void InitiateMainAction()
    {
        PlayerHasAllPuzzlePieces();
    }

    // called when the puzzle mechanic component is enabled.
    public override void OnComponentEnable()
    {
        // N/A
    }

    // called when the puzzle mechanic component is disabled.
    public override void OnComponentDisable()
    {
        // N/A
    }

    // puzzle complete success
    public override bool IsPuzzleComplete()
    {
        return solved;
    }

    // reset puzzle.
    public override void ResetPuzzle()
    {
        solved = false;

        // revert to false.
        if (completePuzzleObject != null)
            completePuzzleObject.SetActive(false);

        // TODO: reactivate all puzzle pieces?
    }


    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
