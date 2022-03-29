using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is for a puzzle.
// maybe try to make a puzzle component?
public class Puzzle : MonoBehaviour
{
    // the name of the puzzle.
    public string puzzleName = "";

    // the description of the puzzle.
    public string puzzleDesc = "";

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // called when the puzzle is completed.
    public virtual void OnPuzzleCompletion()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        
    }
}
