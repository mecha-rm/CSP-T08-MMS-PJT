using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle frame.
public class PuzzleFrame : PuzzleMechanic
{
    // gameplay manager.
    public GameplayManager manager;

    // the game object for the complete puzzle.
    public GameObject completePuzzle;

    // if 'true', the puzzle is consideerd complete.
    public bool complete = false;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // saves the manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // hides the complete puzzle model.
        if (completePuzzle != null)
            completePuzzle.SetActive(complete);

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
        // checks player for puzzle pieces.
        bool result = manager.player.HasAllPuzzlePieces();

        // if this was a success.
        if(result)
        {
            // removes all of the puzzle pieces from the player.
            manager.player.TakeItems(Item.PUZZLE_ID);

            // turns on the model.
            if (completePuzzle != null)
                completePuzzle.SetActive(true);

            // puzzle has been completed.
            if (puzzle != null)
                puzzle.OnPuzzleCompletion();

            Debug.Log("All pieces inputted.");
        }
        else
        {
            // turns off the model.
            if (completePuzzle != null)
                completePuzzle.SetActive(false);

            Debug.Log("Missing pieces still.");
        }

        // TODO: add in puzzle functioanlity.

        return result;
    }

    // puzzle complete success
    public override bool CompleteSuccess()
    {
        return complete;
    }

    // reset puzzle.
    public override void ResetPuzzle()
    {
        // revert to false.
        if (completePuzzle != null)
            completePuzzle.SetActive(false);

        complete = false;
    }


    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
