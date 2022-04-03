﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the puzzle mechanics of the wyvern.
public class Wyvern : PuzzleMechanic
{
    // the descriptor for the wyvern.
    public Descriptor desc;

    // the item attached to the wyvern that is given to the player.
    public NoteItem noteItem;

    // the gameplay manager.
    public GameplayManager manager;

    // if 'true', the wyvern has the treasure.
    public bool hasTreasure = false;

    // prompt line.
    private string promptLine = "I will move once you give me something shiny for my hoard.";

    // confirm line
    private string confirmLine = "As a thank you for this great piece of treasure, I will give you this piece of paper I found on the ground.";

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        // DESCRIPTOR
        // description not set.
        if(desc == null)
        {
            // try to get the component.
            if(!TryGetComponent<Descriptor>(out desc))
            {
                // adds the description component.
                desc = gameObject.AddComponent<Descriptor>();
            }
        }

        // description has been set.
        if(desc != null)
        {
            // name
            if (desc.secondName == "")
                desc.secondName = "Wyvern";

            // description
            if (desc.description == "")
                desc.description = "...";
        }

        // NOTE
        // finds note item.
        if (noteItem == null)
            noteItem = GetComponentInChildren<NoteItem>();

        // the note item has been set.
        if(noteItem != null)
        {
            // sets the note title.
            if (noteItem.Title != "")
                noteItem.Title = "Wyvern Note";

            // sets the text.
            if(noteItem.Text != "")
                noteItem.Text = "Ring bell with floor number.";
        }


        // finds the gameplay manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // updates the descriptor with the current line.
        UpdateDescriptor();
    }

    // mouse clicked down on the wyvern.
    private void OnMouseDown()
    {
        ReceiveTreasure();
    }

    // tries to receive the item from the player.
    public bool ReceiveTreasure()
    {
        // tries to grab the item.
        bool hasItem = manager.player.HasItem(Item.TREASURE_ID);

        // the player has the item.
        if(hasItem)
        {
            // the wyvern now has the treasure.
            manager.player.TakeItem(Item.TREASURE_ID);
            manager.player.GiveItem(noteItem);
            hasTreasure = true;

            // updates the wyvern's line.
            UpdateDescriptor();
        }

        // if the puzzle has been set, tell it the things are complete.
        if (puzzle != null)
            puzzle.OnPuzzleCompletion();

        return hasItem;
    }

    // updates the wyvern's display line.
    public void UpdateDescriptor()
    {
        // the wyvern has the treasure.
        if(hasTreasure)
        {
            // saves the description line.
            desc.description = confirmLine;
        }
        // the wyvern does not have the treasure.
        else
        {
            // saves the prompt line.
            desc.description = promptLine;
        }

        // refreshes the descriptor so that the manager now has the right text.
        manager.RefreshDescriptor();
    }

    // the puzzle was successful.
    public override bool CompleteSuccess()
    {
        throw new System.NotImplementedException();
    }

    // the puzzle is being reset.
    public override void ResetPuzzle()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }
}
