using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// puzzle for the lightbox in room 1.
public class Room1LightboxPuzzle : Puzzle
{
    // the gameplay manager.
    public GameplayManager manager;

    // the keypad for this puzzle.
    public Keypad keypad;

    // the material for the keypad's screen.
    // TODO: get this working for the emissive parameter.
    public Material keypadScreenMat;

    // Start is called before the first frame update
    protected new void Start()
    {
        // call parent's version.
        base.Start();

        // if the manager is not set, find it.
        if(manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // hides the keypad text and makes it inaccessible.
        if (keypad != null)
        {
            // adds the keypad to the mechanics list.
            if (!mechanics.Contains(keypad))
                mechanics.Add(keypad);

            keypad.interactable = false;
            keypad.HideText();
        }

        // disables the emissive property.
        if (keypadScreenMat != null)
        {
            keypadScreenMat.DisableKeyword("_EMISSION");
        }
            
    }

    // called when the puzzle is completed.
    public override void OnPuzzleCompletion()
    {
        // call parent's version.
        base.OnPuzzleCompletion();

        // shows the text on the keypad.
        if (keypad != null)
        {
            keypad.interactable = true;
            keypad.ShowText();
        }

        // enables the emissive property.
        if (keypadScreenMat != null)
        {
            keypadScreenMat.EnableKeyword("_EMISSION");
        }


        //turn on the lights and enable the keypad puzzle
        manager.SetRoomLightingEnabled(true);

        // TODO: turn off puzzle mechanic interactions.
    }

    // called when the puzzle is being reset.
    public override void OnPuzzleReset()
    {
        base.OnPuzzleReset();

        // hides the text on the keypad.
        if (keypad != null)
        {
            keypad.interactable = false;
            keypad.HideText();
        }

        // disables the emissive property.
        if (keypadScreenMat != null)
        {
            keypadScreenMat.DisableKeyword("_EMISSION");
        }

        // turns off the lights.
        manager.SetRoomLightingEnabled(false);
    }

    // Update is called once per frame
    protected new void Update()
    {
        // call parent's version.
        base.Update();
    }
}
