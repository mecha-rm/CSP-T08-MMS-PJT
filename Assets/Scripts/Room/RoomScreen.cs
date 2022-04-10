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

    // the list of puzzles that are in this screen.
    // these are enabled when entering the screen, and disabled when leaving the screen.
    [Tooltip("List of puzzles associated with this screen")]
    public List<Puzzle> puzzles = new List<Puzzle>();

    [Tooltip("Enables and disables puzzle components when entering/leaving the screen if true.")]
    public bool changePuzzlesEnabled = true;

    [Header("Audio")]

    // an audio manager script.
    public AudioManager audioManager;

    // if the audio clip is available, play it.
    [Tooltip("Use audio component if it is set. This will play the audio event saved to this screen.")]
    public bool playAudio = false;

    // plays the entrance audio clip.
    [Tooltip("Play the entrance audio clp if playAudio is set to true.")]
    public bool playEntranceClip = false;

    // entrance audio clip.
    [Tooltip("Clip played when entering the screen.")]
    public AudioClip entranceClip;

    // plays the exit audio clip.
    [Tooltip("Play the exit audio clp if playAudio is set to true.")]
    public bool playExitClip = false;

    // exit audio clip.
    [Tooltip("Clip played when leaving the screen.")]
    public AudioClip exitClip;

    // settings for the room.
    [Header("Settings")]

    // if 'true', this scene uses a perspective camera. If false, this screen using an orthographic camera.
    public bool orthographic = false;

    // if 'true', the screen uses high contrast. This only works if the GameplayManager has 'allowHighContrast' available.
    [Tooltip("If 'true', the screen uses high contrast. This only works if 'allowHighContrast' in the GameplayManager is set to true.")]
    public bool useHighContrast = false;

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

    // Used for activating and deactivating objects in each camera view
    public GameObject[] activeObject = null;

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
            manager = GameplayManager.Current;

        // uses the main audio manager.
        if (audioManager == null)
            audioManager = manager.audioManager;

        // sets default audio clip.
        // this is only set if the entrance clip should be played.
        if (entranceClip == null && playEntranceClip)
            entranceClip = Resources.Load<AudioClip>("Audio/SFXs/SFX_DOOR_OPENING");

        // this is only set if the exit clip should be played.
        if (exitClip == null && playExitClip)
            exitClip = Resources.Load<AudioClip>("Audio/SFXs/SFX_DOOR_OPENING");

        // if the attached camera should be destroyed.
        // these are just for testing purposes, and thus should not be kept.
        if (destroyAttachedCamera)
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

        // if the screen puzzles should be changed when entering.
        if(changePuzzlesEnabled)
        {
            // enables the puzzle mechanics.
            foreach (Puzzle p in puzzles)
                p.enabled = true;
        }

        // This loop activates all accessibility components on objects associated with this screen.
        for (int i = 0; i < activeObject.Length; i++)
        {
            if (activeObject[i].GetComponent<AccessibleButton>() != null)
            {
                activeObject[i].GetComponent<AccessibleButton>().enabled = true;
                Debug.Log("Accessible button " + i);
            }
            else if (activeObject[i].GetComponent<AccessibleButton_3D>() != null)
            {
                activeObject[i].GetComponent<AccessibleButton_3D>().enabled = true;
                Debug.Log("Accessible button 3D " + i);
            }
            else if (activeObject[i].GetComponent<AccessibleLabel>() != null)
            {
                activeObject[i].GetComponent<AccessibleLabel>().enabled = true;
                Debug.Log("Accessible label " + i);
            }
            else if (activeObject[i].GetComponent<AccessibleLabel_3D>() != null)
            {
                activeObject[i].GetComponent<AccessibleLabel_3D>().enabled = true;
                Debug.Log("Accessible label 3D " + i);
            }
        }

        // set the descriptor to that of the new room.
        manager.SetDescriptor(descriptor);

        // if the audio should be played, and the audio manager is set.
        if(playAudio && audioManager != null)
        {
            // plays the entrance clip.
            if(playEntranceClip && entranceClip != null)
                audioManager.PlayAudio(entranceClip);
        }

        // checks for high contrast post processing.
        if(manager.highContrastPostProcessing != null && manager.allowHighContrast)
        {
            // change settings.
            manager.highContrastPostProcessing.SetActive(useHighContrast);
        }
    }

    // called when exiting the screen.
    public virtual void OnScreenExit()
    {
        // exiting the screen, so turn off the room.
        // if (room != null && disableInactiveRoom)
        //     room.gameObject.SetActive(false);

        // if the screen puzzles should be changed when exiting.
        if (changePuzzlesEnabled)
        {
            // disables the puzzle mechanics.
            foreach (Puzzle p in puzzles)
                p.enabled = false;
        }

        // This loop deactivates all accessibility components on objects associated with this screen.
        for (int i = 0; i < activeObject.Length; i++)
        {
            if (activeObject[i].GetComponent<AccessibleButton>() != null)
            {
                activeObject[i].GetComponent<AccessibleButton>().enabled = false;
                Debug.Log("EXIT Accessible button " + i);
            }
            else if (activeObject[i].GetComponent<AccessibleButton_3D>() != null)
            {
                activeObject[i].GetComponent<AccessibleButton_3D>().enabled = false;
                Debug.Log("EXIT Accessible button 3D " + i);
            }
            else if (activeObject[i].GetComponent<AccessibleLabel>() != null)
            {
                activeObject[i].GetComponent<AccessibleLabel>().enabled = false;
                Debug.Log("EXIT Accessible label " + i);
            }
            else if (activeObject[i].GetComponent<AccessibleLabel_3D>() != null)
            {
                activeObject[i].GetComponent<AccessibleLabel_3D>().enabled = false;
                Debug.Log("EXIT Accessible label 3D " + i);
            }
        }

        // if the audio should be played, and the audio manager is set.
        if (playAudio && audioManager != null)
        {
            // plays the exit clip.
            if (playExitClip && exitClip != null)
                audioManager.PlayAudio(exitClip);
        }
    }

    // enables the room screen.
    public void EnableScreen()
    {
        // changes current screen.
        // calls on screen exit if this screen is actually changing.
        if (manager.currentScreen != null && manager.currentScreen != this)
            manager.currentScreen.OnScreenExit();
        
        // switch screen and call enter function.
        manager.currentScreen = this;

        // TODO: check if you're leaving and entering the same screen?
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
        if (backScreen != null)
        {
            // clear out the forward screen.
            backScreen.forwardScreen = null;
        }
            

        SwitchScreen(backScreen);
    }

    // moves to the next screen.
    private void SwitchScreen(RoomScreen nextRoom)
    {
        // no next room.
        if (nextRoom == null)
            return;

        // if the screen is locked.
        if (nextRoom.locked)
        {
            // debug message (TODO: comment out)
            Debug.Log("The desired screen is locked, so you can't access it.");
            
            // sets the descriptor to show that the next room is locked.
            manager.SetDescriptor(nextRoom.descriptor);

            return;
        }

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
