using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the class for the player.
public class Player : MonoBehaviour
{
    // the gameplay manager for the game.
    public GameplayManager manager;

    // the player's inventory.
    public List<Item> inventory = new List<Item>();

    // the mouse light for the mouse.
    public MouseLight mouseLight;

    // Start is called before the first frame update
    void Start()
    {
        // finds the gameplay manager if it's not set.
        if(manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // finds the mouse light in the scene.
        if (mouseLight == null)
            mouseLight = FindObjectOfType<MouseLight>(true);
    }

    // returns 'true' if the mouse light component is enabled
    public bool IsMouseLightEnabled()
    {
        return mouseLight.IsLightEnabled();
    }

    // sets mouse light enabled (enables component).
    public void SetMouseLightEnabled(bool e)
    {
        mouseLight.SetLightEnabled(e);
    }

    // returns 'true' if the player has all of the puzzle pieces.
    public bool HasAllPuzzlePieces()
    {
        // the last piece is already in the frame, so the player must collect it before they can fill the frame.
        // TODO: maybe rework this?
        int ownedPieces = 0;
        string stackId = PuzzlePiece.DefaultStackId;

        // goes through the inventory.
        foreach(Item item in inventory)
        {
            if (item.stackId == stackId)
                ownedPieces++;
        }

        // all puzzle pieces collected.
        return (ownedPieces == GameplayManager.PuzzlePieceCount);
    }

    // Update is called once per frame
    void Update()
    {
        // if the space bar was pressed.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // NOTE: there needs to be player states to know what behaviour the space bar should use.
            manager.SwitchToForwardScreen();
        }
    }
}
