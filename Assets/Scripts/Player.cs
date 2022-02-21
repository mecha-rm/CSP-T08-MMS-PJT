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

    // Start is called before the first frame update
    void Start()
    {
        // finds the gameplay manager if it's not set.
        if(manager == null)
            manager = FindObjectOfType<GameplayManager>();
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
