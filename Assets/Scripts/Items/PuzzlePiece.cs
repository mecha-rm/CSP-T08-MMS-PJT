using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle piece item in the game world.
public class PuzzlePiece : Item
{
    // Start is called before the first frame update
    void Start()
    {
        // name
        if (itemName == "")
            itemName = "Unnumbered Puzzle Piece";

        // description
        if (itemDesc == "")
            itemDesc = "An unnumbered puzzle piece.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
