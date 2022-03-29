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
        if (itemId == "") // stack all puzzle pieces.
            itemId = defaultStackId;

        // item icon not set.
        if (itemIcon == null)
        {
            string file = "Images/Inventory/puzzle_piece_icon";

            // loads resource.
            Sprite temp = Resources.Load<Sprite>(file);

            // checks if valid.
            if(temp != null)
            {
                // if the object can be converted from a sprite.
                itemIcon = temp;
            }
        }
            
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
