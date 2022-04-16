using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the maze puzzle.
public class MazePuzzle : Puzzle
{
    // maze mechanic, which just shows the solution.
    public Maze maze;

    // the amount of wrong answers.
    public int wrongAnswers = 0;

    // the threshold for hints. Once this number is reached the hint is shown.
    public int hintThreshold = 2;

    // use the hint.
    public bool useHint = true;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // grabs the maze in the children.
        if (maze == null)
            maze = GetComponentInChildren<Maze>(true);

        // if the maze is set.
        if (maze != null)
        {
            // not in list, so add it.
            if (!mechanics.Contains(maze))
                mechanics.Add(maze);
        }
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        base.OnPuzzleCompletion();

        // grabs the current player.
        Player player = Player.Current;

        // takes the note from the player.
        if(player != null)
        {
            // This does not check what note to take.
            player.TakeItem(Item.NOTE_ID);
        }
    }

    // calls the on puzzle completion functon with the mechanic that was just completed.
    public override void OnPuzzleCompletion(PuzzleMechanic mechanic)
    {
        // TODO: if you were using multiple mechanics you could check which one finished.
        
        // if the mechanic is a maze box.
        if (mechanic is MazeBox)
        {
            // conversion.
            MazeBox box = mechanic as MazeBox;

            // the door is now open.
            if(box.isOpen)
            {
                // no item, meaning it's the wrong door.
                if (box.storedObject == null)
                {
                    wrongAnswers++;

                    // show the hint.
                    if (useHint && wrongAnswers >= hintThreshold && maze != null)
                    {
                        maze.gameObject.SetActive(true);
                        maze.ResetMechanic();
                    }

                }
                else // has item, meaning it's the right door.
                {
                    // calls the other completion function
                    base.OnPuzzleCompletion();
                }
            } 
        }
    }

    // called when a puzzle fails.
    public override void OnPuzzleFailure()
    {
        // call failure function.
        base.OnPuzzleFailure();
    }

    // called when the puzzle is being reset.
    public override void OnPuzzleReset()
    {
        // reset hte mechanics.
        base.OnPuzzleReset();

        // TODO: fix reset for hint.
        wrongAnswers = 0;

        // can't interact with maze.
        if(maze != null)
        {
            maze.gameObject.SetActive(false);
        }
            
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        // TODO: removed since Maze.cs's mechanic is turned off anyway. If you want to use it, you might need something like this.
        // // maze is not set.
        // if(maze != null)
        // {
        //     // the hint should be used.
        //     if (useHint && wrongAnswers >= hintThreshold)
        //     {
        //         maze.enabled = true;
        //         maze.interactable = true;
        //     }
        //     // the hint should not be used.
        //     else
        //     {
        //         maze.enabled = false;
        //         maze.interactable = false;
        //     }
        // }
        

    }
}
