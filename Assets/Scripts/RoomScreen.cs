using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the room screen.
// the room screen objects are equipped with cameras for positioning. This is just for planning, so disable said component when it's not being used.
public class RoomScreen : MonoBehaviour
{
    // the name of the screen. This can just be a number, or a full-blown name.
    public string screenName = "";

    // the description of the screen. This can be used to describe what the player can view.
    public string screenDesc = "";

    // manager for the game.
    public GameplayManager manager;

    [Header("Screens")]

    // the screen to the left of the current scene.
    // this may be disabled if zoomed in on a given object.
    public RoomScreen leftScreen = null;

    // the screen to the right of the current screen.
    // this may be disabled if zoomed in on a given object.
    public RoomScreen rightScreen = null;

    // goes forward a screen. This is set based on what the player clicked on, and constantly has its value changed.
    public RoomScreen forwardScreen = null;

    // goes back a screen. This is active if zoomed into a specific room portion.
    public RoomScreen backScreen = null;

    // Start is called before the first frame update
    void Start()
    {
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();
    }

    // enables the room screen.
    public void EnableScreen()
    {
        // changes current screen.
        manager.currentScreen = this;

        // makes the player a child of this screen.

        // TODO: should the player be a parent of the screen? Maybe just give the player the transform.
        // making the player a child does move or rotate them from the player's perspective.
        // manager.player.transform.parent = transform;
        // manager.player.transform.position = Vector3.zero;

        // get values.
        manager.player.transform.position = transform.position;
        manager.player.transform.rotation = transform.rotation;
    }

    // switches to the left screen.
    public void SwitchToLeftScreen()
    {
        SwitchScreen(leftScreen);
    }

    // switches to the right screen.
    public void SwitchToRightScreen()
    {
        SwitchScreen(rightScreen);
    }

    // switches to the forward screen.
    public void SwitchToForwardScreen()
    {
        SwitchScreen(forwardScreen);
    }

    // switches to the forward screen.
    public void SwitchToBackScreen()
    {
        SwitchScreen(backScreen);
    }

    // moves to the next screen.
    public void SwitchScreen(RoomScreen nextRoom)
    {
        // enables the next room.
        nextRoom.EnableScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
