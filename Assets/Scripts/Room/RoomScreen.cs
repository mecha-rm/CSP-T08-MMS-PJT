using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the room screen.
// the room screen objects are equipped with cameras for positioning. This is just for planning, so disable said component when it's not being used.
public class RoomScreen : MonoBehaviour
{
    // the room the screen is part of.
    public Room room;

    // TODO: this works, but until we get a more streamlined solution I'm taking this out.
    // if a room is provided, the room activity is changed when entering and leaving a scene.
    // public bool disableInactiveRoom = true;

    // the descriptor for the screen.
    public Descriptor descriptor;

    // manager for the game.
    public GameplayManager manager;

    // settings for the room.
    [Header("Settings")]

    // if 'true', this scene uses a perspective camera. If false, this screen using an orthographic camera.
    public bool orthographic = false;

    // lock settings for the screen.
    [Header("Settings/Locked")]

    // if 'true', the screen is locked and you cannot set it as a forward screen.
    [Tooltip("If true, this screen cannot be switched to.")]
    public bool locked = false;

    // text for a locked screen.
    public string lockedDesc = "This screen is locked.";

    // text for an unlocked screen.
    public string unlockedDesc = "This screen is unlocked.";

    [Header("Settings/Screens")]

    // the screen to the left of the current scene.
    // this may be disabled if zoomed in on a given object.
    public RoomScreen leftScreen = null;

    // the screen to the right of the current screen.
    // this may be disabled if zoomed in on a given object.
    public RoomScreen rightScreen = null;

    // TODO: check if these need to be reset once you leave the screen.
    // goes forward a screen. This is set based on what the player clicked on, and constantly has its value changed.
    // leave this as blank. Maybe hide them from the inspector?
    public RoomScreen forwardScreen = null;

    // goes back a screen. This is active if zoomed into a specific room portion.
    // leave this as blank. Maybe hide them from the inspector?
    public RoomScreen backScreen = null;

    // if 'true', the attached camera script on this screen object is destroyed.
    public bool destroyAttachedCamera = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // if the room is not set, try to grab it.
        if (room == null)
        {
            room = GetComponentInParent<Room>();
        }            

        // descriptor not set.
        if(descriptor == null)
        {
            // grab descriptor.
            descriptor = GetComponent<Descriptor>();

            // add descriptor.
            if (descriptor == null)
                descriptor = gameObject.AddComponent<Descriptor>();
        }

        // gets gameplay manager.
        if (manager == null)
            manager = FindObjectOfType<GameplayManager>();

        // if the attached camera should be destroyed.
        // these are just for testing purposes, and thus should not be kept.
        if(destroyAttachedCamera)
        {
            // grabs the script.
            Camera cam = gameObject.GetComponent<Camera>();

            // destroys the script.
            if (cam != null)
                Destroy(cam);
        }
    }

    // called when entering the screen.
    public virtual void OnScreenEnter()
    {
        // TODO: change camera settings.

        // checks if the lighting for the room is on.
        if(room != null)
        {
            // if the room lighting should change.
            if (manager.IsRoomLightingEnabled() != room.lightsOn)
                room.SetLightingEnabled(room.lightsOn);

            // if the room activity should be changed.
            // if(disableInactiveRoom)
            //     room.gameObject.SetActive(true);
        }
            
    }

    // called whene exiting the screen.
    public virtual void OnScreenExit()
    {
        // exiting the screen, so turn off the room.
        // if (room != null && disableInactiveRoom)
        //     room.gameObject.SetActive(false);
    }

    // enables the room screen.
    public void EnableScreen()
    {
        // changes current screen.
        // called for the screen exit.
        if (manager.currentScreen != null)
            manager.currentScreen.OnScreenExit();
        
        // switch screen and call enter function.
        manager.currentScreen = this;
        OnScreenEnter();

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
        // player needs to click to go forward again.
        // if th screen 
        if (backScreen != null && !locked)
            backScreen.forwardScreen = null;

        SwitchScreen(backScreen);
    }

    // moves to the next screen.
    private void SwitchScreen(RoomScreen nextRoom)
    {
        // if the screen is locked.
        if(locked)
        {
            Debug.Log("The screen is locked, so you can't access it.");
            return;
        }

        // no next room.
        if (nextRoom == null)
            return;

        // enables the next room.
        nextRoom.EnableScreen();

        // removes the forward screen so that the player needs to click on something to move forward again.
        forwardScreen = null;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // the descriptor is set.
        if(descriptor != null)
        {
            // update descriptor every frame.
            descriptor.description = (locked) ? lockedDesc : unlockedDesc;
        }
    }
}
