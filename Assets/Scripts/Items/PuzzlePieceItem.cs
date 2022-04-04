using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle piece item in the game world.
public class PuzzlePieceItem : Item
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // name
        if (descriptor.label == "")
            descriptor.label = "Unnumbered Puzzle Piece";

        // description
        if (descriptor.description == "")
            descriptor.description = "An unnumbered puzzle piece.";

        // stack id
        if (itemId == "") // stack all puzzle pieces.
            itemId = PUZZLE_ID;

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
            return PUZZLE_ID;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
