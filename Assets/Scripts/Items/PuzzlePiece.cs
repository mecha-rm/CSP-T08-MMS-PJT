using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle piece item in the game world.
public class PuzzlePiece : Item
{
    // the default stack id for the puzzle piece.
    private static string defaultStackId = "puzzle-piece";

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // name
        if (itemName == "")
            itemName = "Unnumbered Puzzle Piece";

        // description
        if (itemDesc == "")
            itemDesc = "An unnumbered puzzle piece.";

        // stack id
        if (stackId == "") // stack all puzzle pieces.
            stackId = defaultStackId;
    }

    // returns the default stack id for puzle pieces.
    public static string DefaultStackId
    {
        get
        {
            return defaultStackId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
